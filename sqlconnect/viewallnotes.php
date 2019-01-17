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
	
	$nickname = $_POST["nickname"];
	$cookieFromUser = $_POST["cookie"];
	
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
	
	if($cookieFromUser != $cookieFromServer)
	{
		echo "You are logged out or connection lost";
		exit();
	}
	
	$many = 0;
	
	if($stmt = mysqli_prepare($link, "SELECT `id_note`, `category`, `visibility`, `name`, `description`, `history`, `imageId`, `user`.`nickname` FROM `note` INNER JOIN `user` ON `note`.`id_user` = `user`.`id_user`"))
	{
		//if(mysqli_stmt_bind_param($stmt, "i", $idUser))
		{
			if(mysqli_stmt_execute($stmt))
			{
				if(mysqli_stmt_bind_result($stmt, $id_note, $category, $visibility, $noteName, $description, $history, $imageId, $noteOwner))
				{
					echo "0";
					while (mysqli_stmt_fetch($stmt)) {
						if($visibility == "PUBLIC")
						{
							echo "\t" . $id_note . "\t" . $category. "\t" . $visibility . "\t" . $noteName . "\t" . $description . "\t" . $history . "\t" . $imageId . "\t" . $noteOwner;
							$many++;
						}
					}
					echo "\t\n".$many;
				}
			}
		}
	}
	else
	{
		printf("wong");
	}

	
?>