<?php

	header("Access-Control-Allow-Credentials: true");
	header('Access-Control-Allow-Origin: *');
	header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
	header('Access-Control-Allow-Headers: Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time');

	$user = 'root';
	$password = 'root';
	$db = 'unityproject';
	$host = 'localhost';
	$port = 8889;

	$link = mysqli_init();
	$success = mysqli_real_connect($link, $host, $user, $password, $db, $port);
	
	if(mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code #1 = connection failed
		exit();
	}
	
	//zmienne 
	$nickname = $_POST["nickname"];
	$cookieFromUser = $_POST["cookie"];
	
	$mapName = $_POST["mapName"];
	$mapDescription = $_POST["description"];
	$maxX = $_POST["maxX"];
	$maxY = $_POST["maxY"];
	
	//check if name exists
	$nameCheckQuery = "SELECT id_user, nickname, cookie FROM user WHERE nickname = '".$nickname."';";
	
	$nameCheck = mysqli_query($link, $nameCheckQuery) or die("2: Name check query failed"); //error code #2 - name check query failed
	
	if(mysqli_num_rows($nameCheck)  != 1)
	{
		echo "5: Account with such nickname doesn't exist"; 
		exit();
	}
	
	$existingInfo = mysqli_fetch_assoc($nameCheck);
	$cookieFromServer = $existingInfo["cookie"];
	$idUser = $existingInfo["id_user"];
	
	//printf("Id User: %s", $idUser);
	
	if($cookieFromUser != $cookieFromServer)
	{
		echo "You are logged out or connection lost";
		exit();
	}
	
	$randomIdQuery = "SELECT random_num FROM 
					(SELECT FLOOR(RAND() * 99999) AS random_num 
					UNION 
					SELECT FLOOR(RAND() * 99999) AS random_num ) AS numbers_mst_plus_1 
					WHERE `random_num` NOT IN (SELECT id_note FROM note) LIMIT 1; ";		
	$randomIdResult = mysqli_query($link, $randomIdQuery) or die("Generating note's id failed");
	
	if(mysqli_num_rows($randomIdResult) > 0)
	{
		$rowWithId = mysqli_fetch_row($randomIdResult);
	}
	else
	{
		$rowWithId = 1;
	}
	
	if($stmt = mysqli_prepare($link, "SELECT name FROM note WHERE name=?"))
	{
		mysqli_stmt_bind_param($stmt, "s", $mapName);
		mysqli_stmt_execute($stmt);
		mysqli_stmt_store_result($stmt);
		$number = mysqli_stmt_num_rows($stmt);
		//printf("Rows: %d", $number);
		
		if($number == 0)
		{
			$insertNoteQuery = "INSERT INTO `map`(`id_map`, `id_user_map`, `mapname`, `maxX`, `maxY`, `mapDescription`) 
								VALUES (?, ?, ?, ?, ?, ?);";
			if($stmt1 = mysqli_prepare($link, $insertNoteQuery))
			{
				mysqli_stmt_bind_param($stmt1, "iisiis", $rowWithId[0], $idUser, $mapName, $maxX, $maxY, $mapDescription);
				mysqli_stmt_execute($stmt1);
				echo "0";
			}
		}
		else if($number == 1)
		{
			printf("Mapa o takiej nazwie już istnieje");
			/*
			$updateNoteQuery = "UPDATE `note` 
								SET `category`=?, `visibility`=?, `description`=?, `history`=?, `imageId`=?
								WHERE `name`=?;";			
			if($stmt2 = mysqli_prepare($link, $updateNoteQuery))
			{
				mysqli_stmt_bind_param($stmt2, "ssssis", $category, $visibility, $description, $history, $imageId, $noteName);
				if(mysqli_stmt_execute($stmt2))
				{
					echo "1";
				}
				else
				{
					printf("Bledne query");
				}
			}
			*/
		}
		else
		{
			echo "blad";
		}
	}
	

 
?>