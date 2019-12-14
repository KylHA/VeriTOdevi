CREATE TABLE `firma` (
  `Firma_name` varchar(256) NOT NULL,
  `Firma_email` varchar(256) NOT NULL,
  `Firma_phone_number` varchar(256) NOT NULL,
  `Firma_adres` varchar(800) NOT NULL,
  `Firma_ic_kurumsal` varchar(1500) DEFAULT NULL,
  `Firma_dis_kurumsal` varchar(1500) DEFAULT NULL,
  PRIMARY KEY (`Firma_name`)
)
