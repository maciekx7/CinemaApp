-- MySQL dump 10.13  Distrib 8.0.21, for macos10.15 (x86_64)
--
-- Host: 34.118.42.0    Database: cinemadb
-- ------------------------------------------------------
-- Server version	8.0.18-google

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
SET @MYSQLDUMP_TEMP_LOG_BIN = @@SESSION.SQL_LOG_BIN;
SET @@SESSION.SQL_LOG_BIN= 0;

--
-- GTID state at the beginning of the backup 
--

SET @@GLOBAL.GTID_PURGED=/*!80000 '+'*/ 'b4c48136-9d1d-11eb-8dc5-42010aba0002:1-138614';

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroleclaims`
--

DROP TABLE IF EXISTS `aspnetroleclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroleclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(85) NOT NULL,
  `ClaimType` text,
  `ClaimValue` text,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroleclaims`
--

LOCK TABLES `aspnetroleclaims` WRITE;
/*!40000 ALTER TABLE `aspnetroleclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroleclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroles`
--

DROP TABLE IF EXISTS `aspnetroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroles` (
  `Id` varchar(85) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(85) DEFAULT NULL,
  `ConcurrencyStamp` text,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroles`
--

LOCK TABLES `aspnetroles` WRITE;
/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserclaims`
--

DROP TABLE IF EXISTS `aspnetuserclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(85) NOT NULL,
  `ClaimType` text,
  `ClaimValue` text,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_Client_UserId` FOREIGN KEY (`UserId`) REFERENCES `client` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserclaims`
--

LOCK TABLES `aspnetuserclaims` WRITE;
/*!40000 ALTER TABLE `aspnetuserclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `aspnetuserlogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(85) NOT NULL,
  `ProviderKey` varchar(85) NOT NULL,
  `ProviderDisplayName` text,
  `UserId` varchar(85) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_Client_UserId` FOREIGN KEY (`UserId`) REFERENCES `client` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserlogins`
--

LOCK TABLES `aspnetuserlogins` WRITE;
/*!40000 ALTER TABLE `aspnetuserlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserlogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserroles`
--

DROP TABLE IF EXISTS `aspnetuserroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(85) NOT NULL,
  `RoleId` varchar(85) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_Client_UserId` FOREIGN KEY (`UserId`) REFERENCES `client` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserroles`
--

LOCK TABLES `aspnetuserroles` WRITE;
/*!40000 ALTER TABLE `aspnetuserroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusertokens`
--

DROP TABLE IF EXISTS `aspnetusertokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusertokens` (
  `UserId` varchar(85) NOT NULL,
  `LoginProvider` varchar(85) NOT NULL,
  `Name` varchar(85) NOT NULL,
  `Value` text,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_Client_UserId` FOREIGN KEY (`UserId`) REFERENCES `client` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetusertokens`
--

LOCK TABLES `aspnetusertokens` WRITE;
/*!40000 ALTER TABLE `aspnetusertokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetusertokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `client`
--

DROP TABLE IF EXISTS `client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client` (
  `id` varchar(85) NOT NULL,
  `name` varchar(45) NOT NULL,
  `lastname` varchar(45) NOT NULL,
  `phone` varchar(45) NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(85) DEFAULT NULL,
  `email` varchar(45) NOT NULL,
  `NormalizedEmail` varchar(85) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` text,
  `SecurityStamp` text,
  `ConcurrencyStamp` text,
  `PhoneNumber` text,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` timestamp NULL DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `client_id_UNIQUE` (`id`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client`
--

LOCK TABLES `client` WRITE;
/*!40000 ALTER TABLE `client` DISABLE KEYS */;
INSERT INTO `client` VALUES ('2d68950d-8351-474c-84ce-698d5ea5e150','Lukasz','Sleboda','45785698','lukasz@wp.pl','LUKASZ@WP.PL','lukasz@wp.pl','LUKASZ@WP.PL',0,'AQAAAAEAACcQAAAAEDBZnYy0m8G2Ed5hfQHmGuvRighPJjmnbShbZNPQBfjb1W7sBfxjXApbpla0BSZomw==','BMV4WXWLC36F4G3UVYKJC6LMN5CK4MLR','c2e59a32-f1e1-4f8f-bfce-528479e51157',NULL,0,0,NULL,1,0),('34474afb-4f78-4d3a-b4d3-f1808fd09db6','Stas','Skrzypek','456789123','stas@wp.pl','STAS@WP.PL','stas@wp.pl','STAS@WP.PL',0,'AQAAAAEAACcQAAAAEIzNiK3V/QrgFmVo/cEpQSwbVD9n7e8KOTixTbUM0ZhjyevF3ALX+EU6K/oxmnbqVg==','7WOKZWUKRMIXEOYL3UMC5BEQUX3YOQO3','48269bca-d260-46e4-88d9-b67000683a50',NULL,0,0,NULL,1,0),('ab2e0c22-cb76-42ba-a239-248a26ed9fd2','Maciek','Krysiak','789456258','maciek@wp.pl','MACIEK@WP.PL','maciek@wp.pl','MACIEK@WP.PL',0,'AQAAAAEAACcQAAAAEE1rygVVqOyoPaE+FAn3viwiJPhRpoPE5cWZ8RRi7OwOtEGLC7y7Kvwsg29AOjjK+g==','67NQ7TTFQOS5OTVDXGD5KS25QYHUJTF2','9357ff1f-1f8b-415b-9403-43d8c78ee018',NULL,0,0,NULL,1,0);
/*!40000 ALTER TABLE `client` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `movie`
--

DROP TABLE IF EXISTS `movie`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `movie` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `dubbing` tinyint(1) NOT NULL,
  `subtitles` tinyint(1) NOT NULL,
  `duration` int(11) NOT NULL,
  `description` text NOT NULL,
  `producer` varchar(45) NOT NULL,
  `title` varchar(45) NOT NULL,
  `image_name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `movie_id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `movie`
--

LOCK TABLES `movie` WRITE;
/*!40000 ALTER TABLE `movie` DISABLE KEYS */;
INSERT INTO `movie` VALUES (16,0,1,143,'Kowal Will Turner sprzymierza się z kapitanem Jackiem Sparrowem, by odzyskać swoją miłość - porwaną córkę gubernatora.','Disney','Piraci z Karaibow','piraci_z_karaibow.jpeg'),(17,0,1,162,'Czasy, gdy technologia pozwala na wchodzenie w świat snów. Złodziej Cobb ma za zadanie wszczepić myśl do śpiącego umysłu.','Christopher Nolan','Incepcja','incepcja.jpg'),(18,0,1,115,'Sparaliżowany milioner zatrudnia do opieki młodego chłopaka z przedmieścia, który właśnie wyszedł z więzienia. film','FranceCinema','Nietykalni','nietykalni.jpg'),(19,0,1,115,'Sparaliżowany milioner zatrudnia do opieki młodego chłopaka z przedmieścia, który właśnie wyszedł z więzienia. film','FranceCinema','Ojciec Chrzestny','ojciec_chrzestny.jpg'),(20,0,1,150,'Uzbrojony tylko w jedno słowo — Tenet — bohater przenika w mroczny świat międzynarodowych szpiegów, próbując ocalić ludzkość. Do tego jednak nieodzowne okazuje się skorzystanie ze zjawiska wykraczającego poza czas realny.','Christopher Nolan','Tenet','tenet.jpeg');
/*!40000 ALTER TABLE `movie` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `occasions`
--

DROP TABLE IF EXISTS `occasions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `occasions` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(45) NOT NULL,
  `description` varchar(500) NOT NULL,
  `image` varchar(45) DEFAULT NULL,
  `discount` varchar(5) DEFAULT NULL,
  `price` varchar(6) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `title_UNIQUE` (`title`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `occasions`
--

LOCK TABLES `occasions` WRITE;
/*!40000 ALTER TABLE `occasions` DISABLE KEYS */;
/*!40000 ALTER TABLE `occasions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reservation_type`
--

DROP TABLE IF EXISTS `reservation_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reservation_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` text NOT NULL,
  `discount` int(11) NOT NULL,
  `code` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reservation_type`
--

LOCK TABLES `reservation_type` WRITE;
/*!40000 ALTER TABLE `reservation_type` DISABLE KEYS */;
INSERT INTO `reservation_type` VALUES (5,'normalny',0,NULL),(6,'ulgowy',50,NULL);
/*!40000 ALTER TABLE `reservation_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `screening`
--

DROP TABLE IF EXISTS `screening`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `screening` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `movie_id` int(11) NOT NULL,
  `date` date DEFAULT NULL,
  `time` time NOT NULL,
  `price` int(11) NOT NULL,
  PRIMARY KEY (`id`,`movie_id`),
  UNIQUE KEY `AK_Screening_id` (`id`),
  UNIQUE KEY `screening_id_UNIQUE` (`id`),
  KEY `fk_Screening_Movie1_idx` (`movie_id`),
  CONSTRAINT `movie_id_screening_fk` FOREIGN KEY (`movie_id`) REFERENCES `movie` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=67 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `screening`
--

LOCK TABLES `screening` WRITE;
/*!40000 ALTER TABLE `screening` DISABLE KEYS */;
INSERT INTO `screening` VALUES (39,17,'2021-03-20','15:00:00',30),(40,20,'2021-03-20','18:00:00',25),(41,18,'2021-03-20','21:00:00',25),(42,19,'2021-03-21','13:00:00',22),(43,19,'2021-03-21','16:00:00',22),(44,16,'2021-03-22','19:00:00',30),(45,18,'2021-03-22','13:30:00',30),(46,19,'2021-03-22','16:30:00',30),(47,16,'2021-03-23','10:00:00',30),(48,18,'2021-03-23','13:20:00',30),(49,19,'2021-03-23','16:45:00',30),(50,20,'2021-03-23','20:15:00',30),(51,16,'2021-03-24','10:20:00',30),(52,17,'2021-03-24','13:50:00',30),(53,20,'2021-03-24','17:00:00',30),(54,16,'2021-03-24','20:30:00',30),(55,19,'2021-03-25','10:00:00',30),(56,17,'2021-03-25','13:30:00',30),(57,16,'2021-03-25','17:20:00',30),(58,20,'2021-03-25','21:00:00',30),(59,19,'2021-03-26','11:00:00',30),(60,20,'2021-03-26','13:00:00',30),(61,16,'2021-03-26','17:30:00',30),(62,17,'2021-03-26','22:00:00',30),(63,19,'2021-03-27','11:00:00',30),(64,20,'2021-03-27','13:00:00',30),(65,16,'2021-03-27','17:30:00',30),(66,17,'2021-03-27','22:00:00',30);
/*!40000 ALTER TABLE `screening` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `seat`
--

DROP TABLE IF EXISTS `seat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `seat` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `seat_row` varchar(2) NOT NULL,
  `seat_column` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `seat`
--

LOCK TABLES `seat` WRITE;
/*!40000 ALTER TABLE `seat` DISABLE KEYS */;
INSERT INTO `seat` VALUES (33,'A',1),(34,'A',2),(35,'A',3),(36,'A',4),(37,'B',1),(38,'B',2),(39,'B',3),(40,'B',4),(41,'C',1),(42,'C',2),(43,'C',3),(44,'C',4),(45,'D',1),(46,'D',2),(47,'D',3),(48,'D',4);
/*!40000 ALTER TABLE `seat` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `seat_reserved`
--

DROP TABLE IF EXISTS `seat_reserved`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `seat_reserved` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `seat_id` int(11) NOT NULL,
  `screening_id` int(11) NOT NULL,
  `ticket_id` int(11) NOT NULL,
  PRIMARY KEY (`id`,`ticket_id`,`screening_id`,`seat_id`),
  KEY `screening_id_fk_idx` (`screening_id`),
  KEY `seat_id_fk_idx` (`seat_id`),
  KEY `ticket_id_fk_idx` (`ticket_id`),
  CONSTRAINT `screening_id_seatReservation_fk` FOREIGN KEY (`screening_id`) REFERENCES `screening` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `seat_id_seatReservation_fk` FOREIGN KEY (`seat_id`) REFERENCES `seat` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `ticket_id_seatReservation_fk` FOREIGN KEY (`ticket_id`) REFERENCES `ticket` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `seat_reserved`
--

LOCK TABLES `seat_reserved` WRITE;
/*!40000 ALTER TABLE `seat_reserved` DISABLE KEYS */;
INSERT INTO `seat_reserved` VALUES (36,38,39,57),(37,37,48,58),(38,48,52,59),(39,47,56,60),(40,44,65,61);
/*!40000 ALTER TABLE `seat_reserved` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ticket`
--

DROP TABLE IF EXISTS `ticket`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ticket` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `screening_id` int(11) NOT NULL,
  `reservation_type_id` int(11) NOT NULL,
  `Client_id` varchar(85) NOT NULL,
  `price` varchar(8) NOT NULL,
  `qr_code` varchar(36) DEFAULT NULL,
  PRIMARY KEY (`id`,`screening_id`,`reservation_type_id`,`Client_id`),
  UNIQUE KEY `AK_Ticket_id` (`id`),
  UNIQUE KEY `ticket_id_UNIQUE` (`id`),
  UNIQUE KEY `qr_code_UNIQUE` (`qr_code`),
  KEY `fk_Ticket_Client1_idx` (`Client_id`),
  KEY `reservation_type_id_fk_idx` (`reservation_type_id`),
  KEY `screening_id_idx` (`screening_id`),
  CONSTRAINT `fk_Ticket_Client1` FOREIGN KEY (`Client_id`) REFERENCES `client` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `reservationType_ticket_id_fk` FOREIGN KEY (`reservation_type_id`) REFERENCES `reservation_type` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `screening_id_ticket_fk` FOREIGN KEY (`screening_id`) REFERENCES `screening` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=62 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ticket`
--

LOCK TABLES `ticket` WRITE;
/*!40000 ALTER TABLE `ticket` DISABLE KEYS */;
INSERT INTO `ticket` VALUES (57,39,5,'34474afb-4f78-4d3a-b4d3-f1808fd09db6','30','57.png'),(58,48,5,'34474afb-4f78-4d3a-b4d3-f1808fd09db6','30','58.png'),(59,52,5,'ab2e0c22-cb76-42ba-a239-248a26ed9fd2','30','59.png'),(60,56,6,'ab2e0c22-cb76-42ba-a239-248a26ed9fd2','15','60.png'),(61,65,5,'2d68950d-8351-474c-84ce-698d5ea5e150','30','61.png');
/*!40000 ALTER TABLE `ticket` ENABLE KEYS */;
UNLOCK TABLES;
SET @@SESSION.SQL_LOG_BIN = @MYSQLDUMP_TEMP_LOG_BIN;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-04-22 14:58:11
