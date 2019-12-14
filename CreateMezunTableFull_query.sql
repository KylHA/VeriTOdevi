CREATE TABLE IF NOT EXISTS `mezun` (
  `user_id` int(20) NOT NULL,
  `name` varchar(60) NOT NULL,
  `surname` varchar(30) NOT NULL,
  `id_no` int(11) NOT NULL,
  `Date_of_birth` date DEFAULT NULL,
  `email` varchar(100) NOT NULL,
  `mezunOkul` varchar(200) NOT NULL,
  `mezunDep` varchar(400) NOT NULL,
  `phone_number` varchar(50) DEFAULT NULL,
  `working_firma` varchar(100) DEFAULT NULL,
  `working_pos` varchar(100) DEFAULT NULL,
  `working_area` varchar(100) DEFAULT NULL,
  `languages` varchar(5000) NOT NULL,
  `sertifika` varchar(5535) DEFAULT NULL,
  PRIMARY KEY (`id_no`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `mezun_id_1` FOREIGN KEY (`user_id`) REFERENCES `login` (`user_id`)
) 
