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
	$score = $_POST["score"];
	$cookieFromUser = $_POST["cookie"];
	
	//check if name exists
	$nameCheckQuery = "SELECT nickname, cookie, userExperience FROM user WHERE nickname = '".$nickname."';";
	
	$nameCheck = mysqli_query($link, $nameCheckQuery) or die("2: Name check query failed"); //error code #2 - name check query failed
	
	if(mysqli_num_rows($nameCheck) != 1)
	{
		echo "5: Account with such nickname doesn't exist"; 
		exit();
	}
	
	$existingInfo = mysqli_fetch_assoc($nameCheck);
	$cookieFromServer = $existingInfo["cookie"];
	$userExperience = $existingInfo["userExperience"];
	
	if($cookieFromUser != $cookieFromServer)
	{
		echo "You are logged out or connection lost";
		exit();
	}

	//Experience up
	$userExperience += 100;
	
	$updateUserCookieQuery = "UPDATE `user` SET `userExperience`='".$userExperience."' WHERE nickname = '".$nickname."';";
	mysqli_query($link, $updateUserCookieQuery) or die("4: Updating player's cookie failed");

	echo "0\t" . $userExperience;
?>