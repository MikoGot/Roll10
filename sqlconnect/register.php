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
	
	$email = $_POST["email"];
	$nickname = $_POST["nickname"];
	$password = $_POST["password"];
	
	//check if name exists
	$nameCheckQuery = "SELECT nickname FROM user WHERE nickname = '".$nickname."';";
	
	$nameCheck = mysqli_query($link, $nameCheckQuery) or die("2: Name check query failed"); //error code #2 - name check query failed
	
	if(mysqli_num_rows($nameCheck) > 0)
	{
		echo "3: Name already exists"; // error code #3 - name exists cannot register
		exit();
	}
	
	$randomNumber = 0;
	$randomNumber = rand(0,99999);
	$randomIdQuery = "SELECT FLOOR(RAND() * 99999) AS id_user FROM user 
						WHERE 'id_user' NOT IN (SELECT id_user FROM user)
						LIMIT 1; ";			
	$randomIdResult = mysqli_query($link, $randomIdQuery) or die("Generating new user id failed");
	if(mysqli_num_rows($randomIdResult) > 0)
	{
		$rowWithId = mysqli_fetch_row($randomIdResult);
	}
	else
	{
		$rowWithId = 1;
	}
	
	$salt = "\$5\$rounds=5000\$".$randomNumber.$nickname."\$";
	$hash = crypt($password, $salt);
	
	$insertUserQuery = "INSERT INTO `user` (`id_user`, `nickname`, `email`, `hash`, `salt`, `cookie`, `cookieDateEnd`, `userExperience`)
						VALUES ('".$rowWithId[0]."', '".$nickname."', '".$email."', '".$hash."', '".$salt."', NULL, NULL, '0')";
	mysqli_query($link, $insertUserQuery) or die("4: Insert player query failed");
	
	echo 0;

?>