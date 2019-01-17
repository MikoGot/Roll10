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
	
	$deleteUserQuery = "UPDATE `user` SET `cookie`=NULL WHERE `id_user`=?;";
	if($stmt2 = mysqli_prepare($link, $deleteUserQuery))
	{
		mysqli_stmt_bind_param($stmt2, "i", $idUser);
		if(mysqli_stmt_execute($stmt2))
		{
			echo "1";
		}
		else
		{
			printf("Bledne query");
		}
	}
	else
	{
		echo "blad";
	}
	
	

 
?>