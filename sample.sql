-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 16, 2025 at 06:14 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sample`
--

-- --------------------------------------------------------

--
-- Table structure for table `combobox_options`
--

CREATE TABLE `combobox_options` (
  `option_id` int(11) NOT NULL,
  `combo_name` varchar(50) NOT NULL,
  `option_value` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `combobox_options`
--

INSERT INTO `combobox_options` (`option_id`, `combo_name`, `option_value`) VALUES
(1, 'genre', 'RPG'),
(2, 'genre', 'Shooter'),
(3, 'genre', 'Puzzle'),
(4, 'genre', 'Adventure'),
(5, 'platform', 'PC'),
(6, 'platform', 'PlayStation'),
(7, 'platform', 'Xbox'),
(8, 'platform', 'Nintendo Switch'),
(9, 'status', 'Published'),
(10, 'status', 'Unreleased'),
(11, 'status', 'Beta'),
(12, 'multiplayer', 'Yes'),
(13, 'multiplayer', 'No'),
(14, 'rating', 'E'),
(15, 'rating', 'T'),
(16, 'rating', 'M');

-- --------------------------------------------------------

--
-- Table structure for table `games`
--

CREATE TABLE `games` (
  `game_id` int(11) NOT NULL,
  `title` varchar(100) NOT NULL,
  `genre` varchar(50) NOT NULL,
  `platform` varchar(50) NOT NULL,
  `release_date` date NOT NULL,
  `developer` varchar(100) DEFAULT NULL,
  `publisher` varchar(100) DEFAULT NULL,
  `rating` int(11) DEFAULT NULL,
  `status` varchar(50) DEFAULT NULL,
  `multiplayer` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `games`
--

INSERT INTO `games` (`game_id`, `title`, `genre`, `platform`, `release_date`, `developer`, `publisher`, `rating`, `status`, `multiplayer`) VALUES
(1, 'Minecraft', 'FPS', 'PS5', '2001-07-12', 'Mojang', 'Microsoft', 5, 'Backlog', 'Yes'),
(3, 'adasdasda', 'FPS', 'PS5', '1991-03-15', 'sdas', 'adasd', 5, 'Playing', 'Yes');

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE `students` (
  `id` int(11) NOT NULL,
  `student_no` varchar(50) DEFAULT NULL,
  `last_name` varchar(100) DEFAULT NULL,
  `first_name` varchar(100) DEFAULT NULL,
  `middle_name` varchar(100) DEFAULT NULL,
  `gender` varchar(10) DEFAULT NULL,
  `birthday` date DEFAULT NULL,
  `birthplace` varchar(150) DEFAULT NULL,
  `course_year` varchar(50) DEFAULT NULL,
  `department` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`id`, `student_no`, `last_name`, `first_name`, `middle_name`, `gender`, `birthday`, `birthplace`, `course_year`, `department`) VALUES
(1, '4213412313', 'abdre', 'asddas', 'dsada', 'dsada', '2025-08-25', 'san fernandi', '1asdas', 'SCITE');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `combobox_options`
--
ALTER TABLE `combobox_options`
  ADD PRIMARY KEY (`option_id`);

--
-- Indexes for table `games`
--
ALTER TABLE `games`
  ADD PRIMARY KEY (`game_id`);

--
-- Indexes for table `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `combobox_options`
--
ALTER TABLE `combobox_options`
  MODIFY `option_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `games`
--
ALTER TABLE `games`
  MODIFY `game_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `students`
--
ALTER TABLE `students`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
