<?php

// Do not comment out in productive environment!
require("auth.php");

require("viewmodel.php");

if (!isset($_GET['site'])) 
{
	$site = "home";
}
else
{
	$site = $_GET['site'];
}
//if ($_GET['site'] == "about") $site = "about";

error_reporting(-1);
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);

//include("backend.php");

//rest_get();

//verify_webuser("admin","IchMagKekse");


?>

<!DOCTYPE html>
<html lang="en"><head><meta charset="utf-8">
    <meta charset="utf-8">
    <title>Bootstrap, from Twitter</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Le styles -->
    <link href="css/bootstrap.css" rel="stylesheet">
    <style>
      body {
        padding-top: 60px; /* 60px to make the container go all the way to the bottom of the topbar */
      }
    </style>
    <link href="css/bootstrap-responsive.css" rel="stylesheet">
    <script src="js/jquery.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="//cdnjs.cloudflare.com/ajax/libs/html5shiv/3.6/html5shiv.min.js"></script>
    <![endif]-->

    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="ico/apple-touch-icon-114-precomposed.png">
      <link rel="apple-touch-icon-precomposed" sizes="72x72" href="ico/apple-touch-icon-72-precomposed.png">
                    <link rel="apple-touch-icon-precomposed" href="ico/apple-touch-icon-57-precomposed.png">
                                   <link rel="shortcut icon" href="ico/favicon.png">
  <style type="text/css"></style></head>

  <body cz-shortcut-listen="true">
  
  
  
  <div class="navbar navbar-inverse navbar-fixed-top">
              <div class="navbar-inner">
                <div class="container">
                  <a class="btn btn-navbar collapsed" data-toggle="collapse" data-target=".navbar-inverse-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                  </a>
                  <a class="brand" href="index.php">Convenience</a>
                  <div class="nav-collapse navbar-inverse-collapse collapse" style="height: 0px;">
                    <ul class="nav">
                      <li class="active"><a href="index.php">Home</a></li>
                      <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">User <b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li><a href="index.php?site=viewuser">View All</a></li>
                            <li><a href="index.php?site=edituser">Edit All</a></li>
                            <li><a href="index.php?site=adduser">Add User</a></li>
                            <li role="separator" class="divider"></li>
                            <li><a href="index.php?site=viewmail">View Mails</a></li>
                            <li><a href="index.php?site=editmails">Edit Mails</a></li>
                            <li><a href="index.php?site=addmail">Add Mail</a></li>
                        </ul>
                      </li>
                      <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"  aria-haspopup="true" aria-expanded="true">Products <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="index.php?site=viewproducts">View All</a></li>
                            <li><a href="index.php?site=editproducts">Edit All</a></li>
                            <li><a href="index.php?site=addproduct">Add Product</a></li>
                            <li><a href="index.php?site=pricelist">Pricelist</a></li>
                        </ul>
                   	 </li>
                     <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">Accounting <span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="index.php?site=recent">Recent</a></li>
                            <li><a href="index.php?site=sincekeydate">Since Last KeyDate</a></li>
                            <li role="separator" class="divider"></li>
                            <li><a href="index.php?site=viewkeydates">View KeyDates</a></li>
                            <li><a href="index.php?site=addkeydate">Add KeyDate</a></li>
                            <li role="separator" class="divider"></li>
                            <li><a href="index.php?site=sincedate">Accounting Since</a></li>
                            <li role="separator" class="divider"></li>
                            <li><a href="index.php?site=buydirectly">Add Accounting</a></li>
                        </ul>	
                     </li>
                     <li><a href="index.php?site=about">About</a></li>
                     <li><a href="logout.php" style="color: red">Logout</a></li>
                     
                    </ul>
                    
                  </div><!-- /.nav-collapse -->
                </div>
              </div><!-- /navbar-inner -->
            </div>
  
  

        <div class="container body-content">
			<?php
				switch($site)
				{
					case "about":
						?>
                        <h2> About</h2>
                        <div>This is the fancy Convenience System - you can find it on <a href="https://github.com/auxua/ConvenienceSystem2" target="_blank">Github</a></div>
                        <?php	
						break;
					case "viewuser":
						vm_view_users();
						break;
						
						
					case "viewmail":
						vm_view_mails();
						break;
						
					case "viewproducts":
						vm_view_products();
						break;
						
					case "sincekeydate":
						vm_view_all_debt_since_keydate();
						break;
						
					case "viewkeydates":
						vm_view_keydates();
						break;
						
					case "recent":
						vm_view_recent();
						break;
						
					case "pricelist":
						vm_view_pricelist();
						break;

					case "adduser":
						$status = vm_check_add_user_form();
						$msg = "";
						if ($status != "")
						{
							if ($status->status == true)
								$msg = "User was Added";
							else
								$msg = "Error: ".$status->errorDescription;
						}
						vm_add_user_form($msg);
						break;
						
					case "addmail":
						$status = vm_check_add_mail_form();
						$msg = "";
						if ($status != "")
						{
							if ($status->status == true)
								$msg = "Mail was Added";
							else
								$msg = "Error: ".$status->errorDescription;
						}
						vm_add_mail_form($msg);
						break;
						
					case "addproduct":
						$status = vm_check_add_product_form();
						$msg = "";
						if ($status != "")
						{
							if ($status->status == true)
								$msg = "Product was Added";
							else
								$msg = "Error: ".$status->errorDescription;
						}
						vm_add_product_form($msg);
						break;
						
					case "addkeydate":
						$status = vm_check_add_keydate_form();
						$msg = "";
						if ($status != "")
						{
							if ($status->status == true)
								$msg = "Keydate was Added";
							else
								$msg = "Error: ".$status->errorDescription;
						}
						vm_add_keydate_form($msg);
						break;
						
					case "sincedate":
						if (isset($_GET['mode']))
						{
							$mode = $_GET['mode'];
							$time;
							if ($mode == "yesterday")
								$time = strtotime("-1 day");
							elseif ($mode == "week")
								$time = strtotime("-1 week");
							elseif ($mode == "month")
								$time = strtotime("-1 month");
							else
							{
								$data = vm_check_since_date_form();
							}
							
							if (!isset($data))
							{
								$sdate = date("Y-m-d",$time);
								$data = view_accounting_count_since($sdate);
								var_dump($data);	
							}
							vm_view_since($data);
						}
						else 
						{

							vm_since_date_form();
						}
						break;
						
					case "buydirectly":
						$status = vm_check_buy_form();
						$msg = "";
						if ($status != "")
						{
							if ($status->status == true)
								$msg = "Product was bought";
							else
								$msg = "Error: ".$status->errorDescription;
						}
						vm_buy_form($msg);
						break;
						
					case "edituser":
						$status = vm_edit_users_check();
						$msg = "";
						if ($status != "")
						{
							if ($status->status == true)
								$msg = "Users updated";
							else
								$msg = "Error: ".$status->errorDescription;
						}
						
						vm_edit_users_form($msg);
						break;
						
					case "editproducts":
						$status = vm_edit_products_check();
						$msg = "";
						if ($status != "")
						{
							if ($status->status == true)
								$msg = "Products updated";
							else
								$msg = "Error: ".$status->errorDescription;
						}
						
						vm_edit_products_form($msg);
						break;
						
					case "editmails":
						$status = vm_edit_mails_check();
						$msg = "";
						if ($status != "")
						{
							if ($status->status == true)
								$msg = "Mails updated";
							else
								$msg = "Error: ".$status->errorDescription;
						}
						
						vm_edit_mails_form($msg);
						break;
						
										
					default:
						?>
                        <h2>Welcome</h2>
                        <p>Please select page from menu</p>
                        <?php
				}
				
			?>
            <hr />
            <footer>
                <p>&copy; 2016 - GK</p>
            </footer>
        </div>
  </body>
</html>