-- Phase 3 Migration — Run once before deployment
ALTER TABLE Transactions ADD COLUMN IF NOT EXISTS Reversed TINYINT(1) NOT NULL DEFAULT 0;
ALTER TABLE ItemsRegistry ADD COLUMN IF NOT EXISTS Barcode VARCHAR(100) NULL;
ALTER TABLE ItemsRegistry ADD COLUMN IF NOT EXISTS BarcodeType VARCHAR(20) NOT NULL DEFAULT 'CODE128';
-- Back-fill auto-generated barcodes for existing items
UPDATE ItemsRegistry SET Barcode = CONCAT('OAS-', LPAD(SNo, 4, '0')) WHERE Barcode IS NULL;
