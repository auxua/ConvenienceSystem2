-- phpMyAdmin SQL Dump
-- version 4.6.5.1
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Erstellungszeit: 10. Dez 2016 um 22:41
-- Server-Version: 10.0.27-MariaDB-0+deb8u1
-- PHP-Version: 5.6.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `arno_convenience_test`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_accounting`
--

CREATE TABLE `gk_accounting` (
  `ID` int(11) NOT NULL,
  `date` datetime NOT NULL COMMENT 'time of accounting',
  `user` varchar(255) NOT NULL COMMENT 'username',
  `price` decimal(60,2) NOT NULL,
  `comment` text,
  `device` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_devices`
--

CREATE TABLE `gk_devices` (
  `code` varchar(255) NOT NULL COMMENT '(unique) Device Code',
  `rights` varchar(255) NOT NULL DEFAULT 'READ' COMMENT 'Rights of the Device (READ, FULL...)',
  `comment` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_keydates`
--

CREATE TABLE `gk_keydates` (
  `keydate` datetime NOT NULL,
  `comment` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_mail`
--

CREATE TABLE `gk_mail` (
  `username` varchar(255) NOT NULL COMMENT 'the user name',
  `adress` varchar(255) NOT NULL COMMENT 'the mail adress',
  `active` varchar(8) NOT NULL DEFAULT 'true' COMMENT 'indicate whether the mail should be active'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_pricing`
--

CREATE TABLE `gk_pricing` (
  `ID` int(11) NOT NULL,
  `product` varchar(255) NOT NULL COMMENT 'product name',
  `price` decimal(60,2) NOT NULL COMMENT 'product price',
  `comment` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_user`
--

CREATE TABLE `gk_user` (
  `ID` int(11) NOT NULL,
  `username` varchar(255) NOT NULL COMMENT 'name of the user',
  `debt` decimal(60,2) NOT NULL DEFAULT '0.00' COMMENT 'debt the user has',
  `state` varchar(255) NOT NULL DEFAULT 'active' COMMENT 'state of the user - maybe sth. like inactive, blacked, etc.)',
  `comment` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_webusers`
--

CREATE TABLE `gk_webusers` (
  `username` varchar(255) NOT NULL COMMENT 'the user name of the web users',
  `password` text NOT NULL COMMENT 'the (hashed) password of the user',
  `comment` varchar(255) DEFAULT NULL COMMENT 'a comment on the user'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='the list of the web users of the system';

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gk_web_ip`
--

CREATE TABLE `gk_web_ip` (
  `ID` int(11) NOT NULL COMMENT 'unique ID of entry',
  `ip` varchar(255) NOT NULL COMMENT 'ip mask',
  `comment` varchar(255) NOT NULL COMMENT 'comment on this entry'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `gk_accounting`
--
ALTER TABLE `gk_accounting`
  ADD PRIMARY KEY (`ID`);

--
-- Indizes für die Tabelle `gk_devices`
--
ALTER TABLE `gk_devices`
  ADD PRIMARY KEY (`code`);

--
-- Indizes für die Tabelle `gk_keydates`
--
ALTER TABLE `gk_keydates`
  ADD PRIMARY KEY (`keydate`);

--
-- Indizes für die Tabelle `gk_mail`
--
ALTER TABLE `gk_mail`
  ADD PRIMARY KEY (`username`);

--
-- Indizes für die Tabelle `gk_pricing`
--
ALTER TABLE `gk_pricing`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `product` (`product`);

--
-- Indizes für die Tabelle `gk_user`
--
ALTER TABLE `gk_user`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `username` (`username`);

--
-- Indizes für die Tabelle `gk_webusers`
--
ALTER TABLE `gk_webusers`
  ADD PRIMARY KEY (`username`);

--
-- Indizes für die Tabelle `gk_web_ip`
--
ALTER TABLE `gk_web_ip`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `gk_accounting`
--
ALTER TABLE `gk_accounting`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;
--
-- AUTO_INCREMENT für Tabelle `gk_pricing`
--
ALTER TABLE `gk_pricing`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;
--
-- AUTO_INCREMENT für Tabelle `gk_user`
--
ALTER TABLE `gk_user`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;
--
-- AUTO_INCREMENT für Tabelle `gk_web_ip`
--
ALTER TABLE `gk_web_ip`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT COMMENT 'unique ID of entry', AUTO_INCREMENT=1;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
