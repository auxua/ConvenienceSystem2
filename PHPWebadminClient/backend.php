<?php

require("config.php");

/*****************************************************************************
*
*	Basic REST-Calls
*
*****************************************************************************/

function rest_get($url)
{
	//$service_url = 'http://auxua.de:4014/viewAllUsers.token=foo';
	$service_url = $url;
	$curl = curl_init($service_url);
	curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
	$curl_response = curl_exec($curl);
	if ($curl_response === false) {
		$info = curl_getinfo($curl);
		curl_close($curl);
		die('error occured during curl exec. Additioanl info: ' . var_export($info));
	}
	curl_close($curl);
	$decoded = json_decode($curl_response);
	if (isset($decoded->status) && ($decoded->status == false)) {
		die('error in response: ' . $decoded->errorDesciption);
	}
	//var_dump($decoded);

	return $decoded;
}

function rest_post($url, $data)
{
	//next example will insert new conversation
	$service_url = $url;
	$curl = curl_init($service_url);
	$curl_post_data = $data;
	curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
	curl_setopt($curl, CURLOPT_POST, true);
	curl_setopt($curl, CURLOPT_POSTFIELDS, $curl_post_data);
	$curl_response = curl_exec($curl);
	if ($curl_response === false) {
		$info = curl_getinfo($curl);
		curl_close($curl);
		die('error occured during curl exec. Additioanl info: ' . var_export($info));
	}
	curl_close($curl);
	$decoded = json_decode($curl_response);
	/*if (isset($decoded->status) && ($decoded->status == false)) {
		die('error occured: ' . $decoded->errorDescription);
	}*/
	//echo 'response ok!';
	//var_export($decoded);	
	
	return $decoded;
}

/*****************************************************************************
*
*	Basic Converting functions
*
*****************************************************************************/

function encode_password($pass)
{
	$rawhash = hash("sha512",$pass,true);
	return base64_encode($rawhash);
}

function response_status($response)
{
	return $response->status;
}


/*****************************************************************************
*
*	Backend "high" level functions
*
*****************************************************************************/

// checks user/pw combination. Returns bool representing the success state
function verify_webuser($username, $pass)
{
	$hash = encode_password($pass);
	$data = array(
				"username" => $username,
				"password" => $hash);
	// send to backend
	$response = rest_post(API_BASE_URL."verifyWebUser.token=".DEVICE_CODE,json_encode($data));
	/*if (!response_status($response))
		echo "failed";
	else
		echo "success";*/
		
	return response_status($response);
}

function view_all_users()
{
	$response = rest_get(API_BASE_URL."viewAllUsers.token=".DEVICE_CODE);
	return $response;
}

function view_active_users()
{
	$response = rest_get(API_BASE_URL."viewActiveUsers.token=".DEVICE_CODE);
	return $response;
}

function view_all_products()
{
	$response = rest_get(API_BASE_URL."viewAllProducts.token=".DEVICE_CODE);
	return $response;
}

function view_all_debt_since_keydate()
{
	$response = rest_get(API_BASE_URL."viewAllDebtSinceKeyDate.token=".DEVICE_CODE);
	return $response;
}

function view_accounting_count_since($date)
{
	$response = rest_get(API_BASE_URL."viewAccountingCountSince.date=".$date.".token=".DEVICE_CODE);
	return $response;
}

function view_last_activity($date)
{
	$response = rest_get(API_BASE_URL."viewLastActivity.count=".$date.".token=".DEVICE_CODE);
	return $response;
}

function view_debt_since_keydate($date)
{
	$response = rest_get(API_BASE_URL."viewAllDebtSinceKeyDate.count=".$date.".token=".DEVICE_CODE);
	return $response;
}

function view_accounting_since($date)
{
	$response = rest_get(API_BASE_URL."viewAccountingCountSince.date=".$date.".token=".DEVICE_CODE);
	return $response;
}

function view_products_count_for_user($user)
{
	$response = rest_get(API_BASE_URL."viewProductsCountForUser.user=".$user.".token=".DEVICE_CODE);
	return $response;
}

function view_keydates()
{
	$response = rest_get(API_BASE_URL."viewKeyDates.token=".DEVICE_CODE);
	return $response;
}

function view_mails()
{
	$response = rest_get(API_BASE_URL."viewMails.token=".DEVICE_CODE);
	return $response;
}

function view_recent($count)
{
	$response = rest_get(API_BASE_URL."viewLastActivity.count=".$count.".token=".DEVICE_CODE);	
	return $response; 
}

function add_user($req)
{
	$response = rest_post(API_BASE_URL."addUser.token=".DEVICE_CODE,json_encode($req));
	return $response;
}

function add_mail($req)
{
	$response = rest_post(API_BASE_URL."addMail.token=".DEVICE_CODE,json_encode($req));
	return $response;
}

function add_product($req)
{
	$response = rest_post(API_BASE_URL."addProduct.token=".DEVICE_CODE,json_encode($req));
	return $response;
}

function add_keydate($req)
{
	$response = rest_post(API_BASE_URL."insertKeyDate.token=".DEVICE_CODE,json_encode($req));
	return $response;
}

function buy_directly($req)
{
	$response = rest_post(API_BASE_URL."buyDirectly.token=".DEVICE_CODE,json_encode($req));
	return $response;
}

class UpdateuserRequest
{
	public $dataSet;
}

function update_users($list)
{
	$request = new UpdateuserRequest;
	$request->dataSet = $list;
	$response = rest_post(API_BASE_URL."updateUser.token=".DEVICE_CODE,json_encode($request));
	return $response;
}

class  DeleteUsersRequest
{
	public $dataSet;	
}

function delete_users($list)
{
	$request = new DeleteUsersRequest;
	$request->dataSet = $list;
	$response = rest_post(API_BASE_URL."deleteUser.token=".DEVICE_CODE,json_encode($request));
	return $response;	
}

class UpdateProductRequest
{
	public $dataSet;
}

function update_products($list)
{
	$request = new UpdateProductRequest;
	$request->dataSet = $list;
	$response = rest_post(API_BASE_URL."updateProduct.token=".DEVICE_CODE,json_encode($request));
	return $response;
}

class  DeleteProductRequest
{
	public $dataSet;	
}

function delete_products($list)
{
	$request = new DeleteProductRequest;
	$request->dataSet = $list;
	$response = rest_post(API_BASE_URL."deleteProduct.token=".DEVICE_CODE,json_encode($request));
	return $response;	
}

class UpdateMailRequest
{
	public $dataSet;
}

function update_mails($list)
{
	$request = new UpdateMailRequest;
	$request->dataSet = $list;
	$response = rest_post(API_BASE_URL."updateMails.token=".DEVICE_CODE,json_encode($request));
	return $response;
}

class  DeleteMailRequest
{
	public $dataSet;	
}

function delete_mails($list)
{
	$request = new DeleteMailRequest;
	$request->dataSet = $list;
	$response = rest_post(API_BASE_URL."deleteMails.token=".DEVICE_CODE,json_encode($request));
	return $response;	
}

?>