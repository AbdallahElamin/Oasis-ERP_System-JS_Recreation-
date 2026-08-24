import NextAuth from "next-auth";
import Credentials from "next-auth/providers/credentials";
import bcrypt from "bcryptjs";
import pool from "@/lib/db";

export const { handlers, auth, signIn, signOut } = NextAuth({
  providers: [
    Credentials({
      name: "Credentials",
      credentials: {
        userId: { label: "User ID", type: "text" },
        password: { label: "Password", type: "password" },
      },
      async authorize(credentials) {
        if (!credentials?.userId || !credentials?.password) return null;

        const userId = parseInt(credentials.userId, 10);
        if (isNaN(userId)) return null;

        const rows = await pool.query(
          "SELECT id, fullName, pass, role, isActive FROM Users WHERE id = ? LIMIT 1",
          [userId]
        );

        const user = rows[0];
        if (!user) return null;
        // isActive can be null (no constraint) — treat null as active
        if (user.isActive === false || user.isActive === 0) return null;

        const passwordMatch = await bcrypt.compare(
          String(credentials.password),
          user.pass
        );

        if (!passwordMatch) return null;

        return {
          id: String(user.id),
          name: user.fullName,
          role: user.role,
        };
      },
    }),
  ],
  callbacks: {
    async jwt({ token, user }) {
      if (user) {
        token.id = user.id;
        token.role = user.role;
      }
      return token;
    },
    async session({ session, token }) {
      if (token) {
        session.user.id = token.id;
        session.user.role = token.role;
      }
      return session;
    },
  },
  pages: {
    signIn: "/login",
  },
  session: {
    strategy: "jwt",
  },
});
