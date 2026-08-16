/**
 * Converts a number to its written English equivalent for SDG (Sudanese Pound) currency.
 * Ported from the original VB.NET Other.vb module's SpellNumber function.
 * @param {number} amount - The numeric amount to spell out.
 * @returns {string} The spelled-out amount.
 */
export function spellNumber(amount) {
  if (isNaN(amount) || amount === null || amount === undefined) return "";

  const str = String(Number(amount).toFixed(2));
  const decimalPlace = str.indexOf(".");

  let piastre = "";
  let sdg = "";
  let myNumber = str;

  if (decimalPlace > 0) {
    const centsStr = (str.substring(decimalPlace + 1) + "00").substring(0, 2);
    piastre = getTens(centsStr);
    myNumber = str.substring(0, decimalPlace);
  }

  const place = ["", "", " Thousand ", " Million ", " Billion ", " Trillion "];
  let count = 1;

  while (myNumber !== "") {
    const chunk = myNumber.length > 3 ? myNumber.slice(-3) : myNumber;
    const temp = getHundreds(chunk);
    if (temp !== "") sdg = temp + place[count] + sdg;
    myNumber = myNumber.length > 3 ? myNumber.slice(0, myNumber.length - 3) : "";
    count++;
  }

  if (sdg === "") sdg = "No SDG";
  else if (sdg.trim() === "One") sdg = "One SDG";
  else sdg = sdg.trim() + " SDG";

  if (piastre === "") piastre = " and No Piastre";
  else if (piastre === "One") piastre = " and One Piastre";
  else piastre = " and " + piastre + " Piastres";

  return sdg + piastre + " Only";
}

function getHundreds(num) {
  let result = "";
  if (parseInt(num, 10) === 0) return result;
  num = ("000" + num).slice(-3);
  if (num[0] !== "0") result = getDigit(num[0]) + " Hundred ";
  if (num[1] !== "0") result += getTens(num.substring(1));
  else result += getDigit(num[2]);
  return result.trim();
}

function getTens(tensText) {
  let result = "";
  if (parseInt(tensText[0], 10) === 1) {
    const val = parseInt(tensText, 10);
    const teens = ["Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"];
    result = teens[val - 10] || "";
  } else {
    const tens = ["", "", "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety "];
    result = tens[parseInt(tensText[0], 10)] + getDigit(tensText[1]);
  }
  return result.trim();
}

function getDigit(digit) {
  const digits = ["", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine"];
  return digits[parseInt(digit, 10)] || "";
}

/**
 * Formats a number as a currency string with 2 decimal places.
 * @param {number} num
 * @returns {string}
 */
export function formatCurrency(num) {
  if (isNaN(num) || num === null) return "0.00";
  return Number(num).toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 });
}

/**
 * Returns today's date formatted as YYYY-MM-DD.
 * @returns {string}
 */
export function todayStr() {
  return new Date().toISOString().split("T")[0];
}
