<?php

require("backend.php");


class BaseResponse
{
	public $status;
	public $errorDescription;
	public $comment;	
}

/**************************************
*	Exports
**************************************/

function vm_export_users($delimiter=";")
{
	$result = view_all_users();
	$dataset = $result->dataSet;

	$csvexport = "\xEF\xBB\xBF"."Username".$delimiter."Debt".$delimiter."State".$delimiter."Comment"."\n";
	

	foreach ($dataset as $user)
	{
		$csvexport .= $user->username . $delimiter;
		$csvexport .= money_format('%.2n', $user->debt) . $delimiter;
		$csvexport .= $user->state . $delimiter;
		$csvexport .= $user->comment . $delimiter . "\n";
	}
	header('Content-Encoding: UTF-8');
	header("Content-disposition: attachment; filename=users.csv");
	header("Content-type: text/csv; charset=UTF-8");
	echo($csvexport);
}

function vm_export_prodcuts($delimiter=";")
{
	$result = view_all_products();
	$dataset = $result->dataSet;

	$csvexport = "\xEF\xBB\xBF"."Product".$delimiter."Price".$delimiter."Comment"."\n";
	

	foreach ($dataset as $user)
	{
		$csvexport .= $user->product . $delimiter;
		$csvexport .= money_format('%.2n', $user->price) . $delimiter;
		$csvexport .= $user->comment . $delimiter . "\n";
	}
	header('Content-Encoding: UTF-8');
	header("Content-disposition: attachment; filename=products.csv");
	header("Content-type: text/csv; charset=UTF-8");
	echo($csvexport);
	
}


/**************************************
*	View based ViewModels
**************************************/

function vm_view_users()
{
	$result = view_all_users();
	$dataset = $result->dataSet;

	?>
    
    <h2> All User </h2>
    
	<table class="table table-striped table-bordered">
		<tbody><tr><th>#</th><th>Username</th><th>Debt</th><th>State</th><th>Comment</th><th>Actions</th></tr>
                
	<?php
                
                


	foreach ($dataset as $user)
	{
		?>
		<tr>
            <td><span><?php echo $user->iD; ?></span></td>
            <td><span><?php echo $user->username; ?></span></td>
            <td><span><?php echo $user->debt; ?></span></td>
            <td><span><?php echo $user->state ?></span></td>
            <td><span><?php echo $user->comment; ?></span></td>
			<td><span><a href="index.php?site=viewaccountinguser&user=<?php echo $user->iD; ?>" class="btn btn-info" role="button">Accounting</a>&nbsp;</span></td>
        </tr>      
        <?php  
	}
	
	?>
    </tbody></table>
    <?php
}

function vm_view_pricelist()
{
	$result = view_all_products();
	$dataset = $result->dataSet;

	?>
    
    <h2> Pricelist </h2>
    
	<table class="table table-striped table-bordered">
        <tbody><tr><th>Product name</th><th>Price</th></tr>
                
	<?php
                
                


	foreach ($dataset as $product)
	{
		?>
		<tr>
            <td><span><?php echo $product->product; ?></span></td>
            <td><span><?php echo $product->price; ?></span></td>
        </tr>      
        <?php  
	}
	
	?>
    </tbody></table>
    <?php
}

function vm_view_mails()
{
	$result = view_mails();
	$dataset = $result->dataSet;

	?>
    
    <h2> All Mails </h2>
    
	<table class="table table-striped table-bordered">
        <tbody><tr><th>Username</th><th>Adress</th><th>active?</th></tr>
                
	<?php
                
                


	foreach ($dataset as $user)
	{
		?>
		<tr>
            <td><span><?php echo $user->username; ?></span></td>
            <td><span><?php echo $user->adress; ?></span></td>
            <td><span><?php echo $user->active; ?></span></td>
        </tr>      
        <?php  
	}
	
	?>
    </tbody></table>
    <?php
}


function vm_view_products()
{
	$result = view_all_products();
	$dataset = $result->dataSet;

	?>
    
    <h2> All Products </h2>
    
	<table class="table table-striped table-bordered">
        <tbody><tr><th>ID</th><th>Product</th><th>price</th><th>Comment</th></tr>
                
	<?php
                
                


	foreach ($dataset as $user)
	{
		?>
		<tr>
            <td><span><?php echo $user->iD; ?></span></td>
            <td><span><?php echo $user->product; ?></span></td>
            <td><span><?php echo $user->price; ?></span></td>
            <td><span><?php echo $user->comment; ?></span></td>
        </tr>      
        <?php  
	}
	
	?>
    </tbody></table>
    <?php
}

function vm_view_all_debt_since_keydate()
{
	$result = view_all_debt_since_keydate();
	$dataset = $result->dataSet;

	?>
    
    <h2> All Debt Since Last Keydate </h2>
    
	<table class="table table-striped table-bordered">
        <tbody><tr><th>Username</th><th>Debt</th></tr>
                
	<?php
                
                


	foreach ($dataset as $user)
	{
		?>
		<tr>
            <td><span><?php echo $user->username; ?></span></td>
            <td><span><?php echo $user->debt; ?></span></td>
        </tr>      
        <?php  
	}
	
	?>
    </tbody></table>
    <?php
}

function vm_view_keydates()
{
	$result = view_keydates();
	$dataset = $result->dataSet;

	?>
    
    <h2> All Keydates </h2>
    
	<table class="table table-striped table-bordered">
        <tbody><tr><th>keydate</th><th>comment</th></tr>
                
	<?php
                
                


	foreach ($dataset as $user)
	{
		?>
		<tr>
            <td><span><?php echo $user->keydate; ?></span></td>
            <td><span><?php echo $user->comment; ?></span></td>
        </tr>      
        <?php  
	}
	
	?>
    </tbody></table>
    <?php
}

class AddFirewallRequest
{
	public $ip;
	public $comment;
}

function vm_view_firewall()
{
	// cehck for deletions
	if (isset($_GET['delete']))
	{
		$id = $_GET['delete'];
		$ids = array();
		$ids[] = $id;
		delete_firewall($ids);
	}
	
	if (isset($_POST['ip']))
	{
		// Adding a new rule
		$ip = $_POST['ip'];
		$comment = $_POST['comment'];
		$add = new AddFirewallRequest;
		$add->comment = $comment;
		$add->ip = $ip;
		add_firewall($add);
	}
	
	$result = view_firewall();
	$dataset = $result->dataSet;
	
	$ip = view_ip();

	?>
    
    <h2> Integrated µFirewall </h2>
    
	<table class="table table-striped table-bordered">
		<tbody><tr><th>ID</th><th>IP (range)</th><th>comment</th><th>Actions</th></tr>
                
	<?php
                
                


	foreach ($dataset as $user)
	{
		?>
		<tr>
            <td><span><?php echo $user->iD; ?></span></td>
            <td><span><?php echo $user->ip; ?></span></td>
            <td><span><?php echo $user->comment; ?></span></td>
            <td><span><a href="index.php?site=viewfirewall&delete=<?php echo $user->iD; ?>" class="btn btn-danger" role="button" onclick="return confirm('Are you sure?')">Delete</a>&nbsp;</span></td>
        </tr>      
        <?php  
	}
	
	?>
    </tbody></table>
    
	<h5> Current connection via: <?php echo $ip; ?></h5>
   
   	<form class="form-horizontal" action="index.php?site=viewfirewall" method="post">
	<fieldset>

	<!-- Form Name -->
	<legend>Add Firewall Rule</legend>

		<!-- Text input-->
		<div class="control-group">
		<label class="control-label" for="ip">IP Range</label>
		<div class="controls">
		<input id="ip" name="ip" type="text" placeholder="127.0.0.1" class="input-xlarge" required="">
		<p class="help-block">You may single ips or subnets (e.g. 127.0.*)</p>
		</div>
		</div>

		<!-- Text input-->
		<div class="control-group">
		<label class="control-label" for="comment">Comment</label>
		<div class="controls">
		<input id="comment" name="comment" type="text" placeholder="home ip" class="input-xlarge">

		</div>
		</div>

		<!-- Button -->
		<div class="control-group">
		<label class="control-label" for="submit"></label>
		<div class="controls">
		<button id="submit" name="submit" class="btn btn-success">submit</button>
		</div>
		</div>

	</fieldset>
	</form>

    <?php
}

function vm_view_recent($count = 10, $message = "")
{
	$result = view_recent($count);
	$dataset = $result->dataSet;

	?>
    
    <h2> Recent Activities </h2>
    
    <?php 
	
	if (!empty($message))
	{
		?>
			<h5 style="color: #8B0000"> <?php echo $message; ?></h5>
		<?php
	}
	
	?>
    
	<table class="table table-striped table-bordered">
		<tbody><tr><th>#</th><th>Date</th><th>User</th><th>Price</th><th>Comment</th><th>Actions</th></tr>
                
	<?php
                
                


	foreach ($dataset as $user)
	{
		?>
		<tr>
            <td><span><?php echo $user->iD; ?></span></td>
            <td><span><?php echo $user->{'date'}; ?></span></td>
            <td><span><?php echo $user->user; ?></span></td>
            <td><span><?php echo $user->price; ?></span></td>
            <td><span><?php echo $user->comment; ?></span></td>
            <td><span><a href="index.php?site=revertaccounting&acc=<?php echo $user->iD; ?>" class="btn btn-danger" role="button" onclick="return confirm('Are you sure?')">Revert</a>&nbsp;</span></td>
            
        </tr>      
        <?php  
	}
	
	?>
    </tbody></table>
    
    <!-- Show custom number of recent items -->
    <form class="form-horizontal" action="index.php?site=recent" method="post">
	<fieldset>

	<!-- Form Name -->
	<legend>Show Custom Amount</legend>

	<!-- Text input-->
	<div class="control-group">
	  <label class="control-label" for="amount">Amount of Items &nbsp; </label>
	  <!--<div class="controls">-->
		<input id="amount" name="amount" type="text" placeholder="10" class="input-small" required="">

	  <!--</div>-->
	<!--</div>
	<div class="control-group">-->
	  <!--<label class="control-label" for="submit"></label>-->
	  <!--<div class="controls">-->
		<button id="submit" name="submit" class="btn btn-success">show</button>
	  <!--</div>-->
	</div>

	</fieldset>
	</form>
    
    <?php
}

function vm_view_accounting_user($id, $message="")
{
	$result = view_accounting_user($id);
	$dataset = $result->dataSet;
	
	

	?>
    
    <h2> Accounting for <?php echo $dataset[0]->user; ?></h2>
    
    <?php 
	
	if (!empty($message))
	{
		?>
			<h5 style="color: #8B0000"> <?php echo $message; ?></h5>
		<?php
	}
	
	?>
    
	<table class="table table-striped table-bordered">
		<tbody><tr><th>#</th><th>Date</th><th>User</th><th>Price</th><th>Comment</th><th>Actions</th></tr>
                
	<?php
                
                


	foreach ($dataset as $user)
	{
		?>
		<tr>
            <td><span><?php echo $user->iD; ?></span></td>
            <td><span><?php echo $user->{'date'}; ?></span></td>
            <td><span><?php echo $user->user; ?></span></td>
            <td><span><?php echo $user->price; ?></span></td>
            <td><span><?php echo $user->comment; ?></span></td>
            <!-- insert revert here -->
            <td><span><a href="index.php?site=revertaccounting&acc=<?php echo $user->iD; ?>&user=<?php echo $_GET['user']; ?>" class="btn btn-danger" role="button" onclick="return confirm('Are you sure?')">Revert</a>&nbsp;</span></td>
            
        </tr>      
        <?php  
	}
	
	?>
    </tbody></table>
    <?php
}

function vm_revert_accounting()
{
	$useRecent = !isset($_GET['user']);
	
	// perform revert operation
	$succ = revert($_GET['acc']);
	
	$message = "";
	
	if ($succ)
		$message = "Reverted Element";
	else
		$message = "Could not revert!";
	
	if (!$useRecent)
		vm_view_accounting_user($_GET['user'],$message);
	else
		vm_view_recent(10,$message);
}

function vm_view_since($data)
{
	$dataset = $data->dataSet;
	
	?>
       <div id="MainContent_AccountingTable">
        <h2>Activities</h2>
        <table class="table table-striped table-bordered">
        <tr><th>Product name</th><th>#</th></tr>
            
            <?php
			
			foreach ($dataset as $prod)
			{
				?>
            	<tr>
                    <td><span id="MainContent_repAccounting_labelID_0"><?php print $prod->product; ?></span></td>
                    <td><span id="MainContent_repAccounting_labelDate_0"><?php print $prod->amount; ?></span></td>
                </tr>    
                
                <?php
			}
            
                ?>
                
         </table>
        </div>
        
        <?php
 	
}

/**************************************
*	Forms for Adding
**************************************/

class CreateUserRequest 
{
	public $user;
	public $comment;
	public $state;	
}

// Creates the Form for Adding a User
function vm_add_user_form($status="")
{
	?> <h2> Add User </h2> 
    
    	<div class="container" style="min-width:100px; max-width:300px; ">
        <form method="post" action="index.php?site=adduser">
        <fieldset>
        
        <!-- Form Name -->
        <legend>Add a User</legend>
        
        <!-- Text input-->
        <div class="control-group">
          <label class="control-label" for="inputUser"></label>
          <div class="controls">
            <input id="inputUser" name="inputUser" type="text" placeholder="Username" class="input-xlarge" required="">
            
          </div>
        </div>
        
        <!-- Multiple Checkboxes (inline) -->
        <div class="control-group">
          <label class="control-label" for="checkState"></label>
          <div class="controls">
            <label class="checkbox inline" for="checkState-0">
              <input type="checkbox" name="checkState" id="checkState-0" value="Active">
              Active
            </label>
          </div>
        </div>
        
        <!-- Text input-->
        <div class="control-group">
          <label class="control-label" for="inputComment"></label>
          <div class="controls">
            <input id="inputComment" name="inputComment" type="text" placeholder="Comment" class="input-xlarge">
            
          </div>
        </div>
        
        <!-- Button -->
        <div class="control-group">
          <label class="control-label" for="singlebutton"></label>
          <div class="controls">
            <button id="singlebutton" name="singlebutton" class="btn btn-primary">Confirm</button>
          </div>
        </div>
        
        </fieldset>
        </form>


    
        <span style="color:darkred"><?php print $status; ?></span>
        </div>
        
        <?php
	
}

// Checking the state of the form for adding user. Provides status string, when finished
function vm_check_add_user_form()
{
	if (isset($_POST['inputUser']))
	{
		$req = new CreateUserRequest;
		$req->comment = $_POST['inputComment'];
		$req->state = ($_POST['checkState'] == "Active");
		$req->user = $_POST['inputUser'];
		
		$result = add_user($req);
		
		return $result;
	}
	
	return "";
}


class CreateMailRequest 
{
	public $username;
	public $adress;	
}

// Creates the Form for Adding a User
function vm_add_mail_form($status="")
{
	?> <h2> Add Mail </h2> 
    
    	<div class="container" style="min-width:100px; max-width:300px; ">
        <form method="post" action="index.php?site=addmail">
        <fieldset>
        
        <!-- Form Name -->
        <legend>Add a Mail Connection</legend>
        
        <!-- Text input-->
        <div class="control-group">
          <label class="control-label" for="inputUser"></label>
          <div class="controls">
            <input id="inputUser" name="inputUser" type="text" placeholder="Username" class="input-xlarge" required="">
            
          </div>
        </div>
        
        
        <!-- Text input-->
        <div class="control-group">
          <label class="control-label" for="inputAdress"></label>
          <div class="controls">
            <input id="inputAdress" name="inputAdress" type="text" placeholder="Mail adress" class="input-xlarge" required="">
            
          </div>
        </div>
        
        <!-- Button -->
        <div class="control-group">
          <label class="control-label" for="singlebutton"></label>
          <div class="controls">
            <button id="singlebutton" name="singlebutton" class="btn btn-primary">Confirm</button>
          </div>
        </div>
        
        </fieldset>
        </form>


    
        <span style="color:darkred"><?php print $status; ?></span>
        </div>
        
        <?php
	
}

// Checking the state of the form for adding user. Provides status string, when finished
function vm_check_add_mail_form()
{
	if (isset($_POST['inputUser']))
	{
		$req = new CreateMailRequest;
		$req->adress = $_POST['inputAdress'];
		$req->username = $_POST['inputUser'];
		
		$result = add_mail($req);
		
		return $result;
	}
	
	return "";
}



class CreateProductRequest 
{
	public $product;
	public $price;	
	public $comment;	
}

// Creates the Form for Adding a User
function vm_add_product_form($status="")
{
	?> <h2> Add Mail </h2> 
    
    	<div class="container" style="min-width:100px; max-width:300px; ">
        <form method="post" action="index.php?site=addproduct">
        <fieldset>
        
        <!-- Form Name -->
        <legend>Add a New Product</legend>
        
        <!-- Text input-->
        <div class="control-group">
          <label class="control-label" for="inputProduct"></label>
          <div class="controls">
            <input id="inputProduct" name="inputProduct" type="text" placeholder="Product Name" class="input-xlarge" required="">
            
          </div>
        </div>
        
        <div class="control-group">
          <label class="control-label" for="inputPrice"></label>
          <div class="controls">
            <input id="inputPrice" name="inputPrice" type="text" placeholder="Product Price" class="input-xlarge" required="">
            
          </div>
        </div>
        
        <!-- Text input-->
        <div class="control-group">
          <label class="control-label" for="inputComment"></label>
          <div class="controls">
            <input id="inputComment" name="inputComment" type="text" placeholder="Comment" class="input-xlarge">
            
          </div>
        </div>
        
        <!-- Button -->
        <div class="control-group">
          <label class="control-label" for="singlebutton"></label>
          <div class="controls">
            <button id="singlebutton" name="singlebutton" class="btn btn-primary">Confirm</button>
          </div>
        </div>
        
        </fieldset>
        </form>


    
        <span style="color:darkred"><?php print $status; ?></span>
        </div>
        
        <?php
	
}


// Checking the state of the form for adding user. Provides status string, when finished
function vm_check_add_product_form()
{
	if (isset($_POST['inputProduct']))
	{
		$req = new CreateProductRequest;
		$req->comment = $_POST['inputComment'];
		$price = floatval(str_replace(",",".",$_POST['inputPrice']));
		if ($price == 0)
		{
			$res = new BaseResponse;
			$res->status = false;
			$res->errorDescription = "Please use valid numbers for price parameter";
			return $res;
		}
		$req->price = $price;
		$req->product = $_POST['inputProduct'];
		
		$result = add_product($req);
		
		return $result;
	}
	
	return "";
}


class CreateKeydateRequest 
{
	public $comment;	
}

// Creates the Form for Adding a User
function vm_add_keydate_form($status="")
{
	?> <h2> Add Keydate </h2> 
    
    	<div class="container" style="min-width:100px; max-width:300px; ">
        <form method="post" action="index.php?site=addkeydate">
        <fieldset>
        
        <!-- Form Name -->
        <legend>Add a New Keydate</legend>
        
        
        <!-- Text input-->
        <div class="control-group">
          <label class="control-label" for="inputComment"></label>
          <div class="controls">
            <input id="inputComment" name="inputComment" type="text" placeholder="Comment" class="input-xlarge" required="">
            
          </div>
        </div>
        
        <!-- Button -->
        <div class="control-group">
          <label class="control-label" for="singlebutton"></label>
          <div class="controls">
            <button id="singlebutton" name="singlebutton" class="btn btn-primary">Confirm</button>
          </div>
        </div>
        
        </fieldset>
        </form>


    
        <span style="color:darkred"><?php print $status; ?></span>
        </div>
        
        <?php
	
}


// Checking the state of the form for adding user. Provides status string, when finished
function vm_check_add_keydate_form()
{
	if (isset($_POST['inputComment']))
	{
		$req = new CreateKeydateRequest;
		$req->comment = $_POST['inputComment'];
		
		$result = add_keydate($req);
		
		return $result;
	}
	
	return "";
}





// Creates the Form for Adding a User
function vm_since_date_form($status="")
{
	?> <h2> Items since Keydate </h2> 
    
    	<div class="container" style="min-width:100px; max-width:300px; ">
        <form method="post" action="index.php?site=sincedate&mode=custom">
        <fieldset>
        
        <!-- Form Name -->
        <legend>Show Items since Keydate</legend>
        
        
        <!-- Text input-->
        <div class="control-group">
          <!--<a href="index.php?site=sincedate&mode=yesterday" class="btn btn-info" role="button">Yesterday</a>&nbsp;-->
           <!--<a href="#" data-id="yesterday" class="btn danger btn-info confirm-delete" role="button">Yesterday</a>&nbsp;-->
           <a href="index.php?site=sincedate&mode=yesterday" class="btn btn-info" role="button">Yesterday</a>&nbsp;
            <a href="index.php?site=sincedate&mode=week" class="btn btn-info" role="button">Last Week</a>&nbsp;
            <a href="index.php?site=sincedate&mode=month" class="btn btn-info" role="button">Last Month</a>&nbsp;
            <a href="index.php?site=sincekeydate" class="btn btn-info" role="button">Last Keydate</a>&nbsp;<br /><br />
            <label for="inputDate" class="sr-only">Since (use text field)</label>
            <input name="inputDate" type="text" id="inputDate" style="width:100%" class="form-control" placeholder="2015-12-31" required="" autofocus />
           
          </div>
        
        <!-- Button -->
        <div class="control-group">
          <label class="control-label" for="singlebutton"></label>
          <div class="controls">
            <button id="singlebutton" name="singlebutton" class="btn btn-success">Show</button>
          </div>
        </div>
        
        </fieldset>
        </form>


    
        <span style="color:darkred"><?php print $status; ?></span>
        </div>
        
        <?php
	
}


// Checking the state of the form for adding user. Provides status string, when finished
function vm_check_since_date_form()
{
	if (isset($_POST['inputDate']))
	{
		$result = view_accounting_since($_POST['inputDate']);
		return $result;
	}
	
	return "";
}


/**************************************
* Buy Directly
**************************************/

class BuyDirectlyRequest
{
	public $username;
	public $price;
	public $comment;	
}

function vm_check_buy_form()
{
	if (isset($_POST['UserSelect']))
	{
		$user = $_POST['UserSelect'];
		if (!empty($_POST['inputCustomReason']))
		{
			// Custom Buy
			$reason = $_POST['inputCustomReason'];
			$price = floatval(str_replace(",",".",$_POST['inputCustomPrice']));
			
			$req = new BuyDirectlyRequest;
			$req->comment = $reason;
			$req->price = $price;
			$req->username = $user;
			
			return buy_directly($req);
		}
		else
		{
			// predefined Products
			$products = view_all_products()->dataSet;
			$reason = $_POST['ReasonSelectServer'];
			$pr = NULL;
			foreach ($products as $prod)
			{
				if ($prod->product == $reason)
				{
					$pr = $prod->price;
					break;
				}
			}
			if ($pr == NULL) 
			{
				$a = new BaseResponse;
				$a->status=false;
				$a->errorDescription = "Could not find product";
				return $a;
			}
			$req = new BuyDirectlyRequest;
			$req->comment = $reason;
			$req->price = floatval(str_replace(",",".",$pr));
			$req->username = $user;
			return buy_directly($req);
		}
	}
}

function vm_buy_form($status = "")
{
	$users = view_active_users();
	$products = view_all_products();
	
	$userdata = $users->dataSet;
	$productdata = $products->dataSet;
	
	?> <h2> Buy Directly </h2> 
    
    	<div class="container" style="min-width:100px; max-width:300px; ">
        <form method="post" action="index.php?site=buydirectly">
        <fieldset>
        
        <!-- Form Name -->
        <legend>Add new Accounting Element</legend>
        
        
        
        
        <!-- Text input-->
        <div class="control-group">
          <label for="UserSelect" class="sr-only">User:</label>
            <select name="UserSelect" id="UserSelect" class="form-control" style="width: 100%">
				<?php
				
				foreach($userdata as $user)
				{
					?>
                    <option value="<?php print $user->username; ?>"><?php print $user->username; ?></option>
					<?php
				}
				
				?>
          	
            </select> <br />
            
            <label for="ReasonSelectServer" class="sr-only">Product:</label>
            <select name="ReasonSelectServer" id="ReasonSelectServer" class="form-control" style="width: 100%">
            
            <?php
				
				foreach($productdata as $prod)
				{
					?>
                    <option value="<?php print $prod->product; ?>"><?php print $prod->product." - ".$prod->price; ?></option>
					<?php
				}
				
			?>	
            </select> <br />
            <hr />
			<p><p>Or use custom values <br /> (ignoring the product selection above)</p></p>
            <hr />
			<input name="inputCustomReason" type="text" id="inputCustomReason" class="form-control" placeholder="Product/Comment" style="width: 100%" /><br />
            <input name="inputCustomPrice" type="text" id="inputCustomPrice" class="form-control" placeholder="0.20" style="width: 100%" /><br />
        </div>

        
        <!-- Button -->
        <div class="control-group">
          <label class="control-label" for="singlebutton"></label>
          <div class="controls">
            <button id="singlebutton" name="singlebutton" class="btn btn-primary">Confirm</button>
          </div>
        </div>
        
        </fieldset>
        </form>


    
        <span style="color:darkred"><?php print $status; ?></span>
        </div>
        
        <?php
}


/************************************
* Edit Forms
************************************/

function startsWith($haystack, $needle) {
    // search backwards starting from haystack length characters from the end
    return $needle === "" || strrpos($haystack, $needle, -strlen($haystack)) !== false;
}

function endsWith($haystack, $needle) {
    // search forward starting from end minus needle length characters
    return $needle === "" || (($temp = strlen($haystack) - strlen($needle)) >= 0 && strpos($haystack, $needle, $temp) !== false);
}


class User
{
	public $iD;
	public $username;
	public $debt;
	public $state;
	public $comment;
}

function vm_edit_users_form($status="")
{
	// Fake data
	/*$dataset = array();
	$user0 = new User;
	$user0->comment = "comment0";
	$user0->debt = 0.1;
	$user0->iD = 0;
	$user0->state = "active";
	$user0->username = "0 user";
	$dataset[0] = $user0;
	
	$user1 = new User;
	$user1->comment = "comment1";
	$user1->debt = 1.1;
	$user1->iD = 1;
	$user1->state = "inactive";
	$user1->username = "1 user";
	$dataset[1] = $user1;

	$user2 = new User;
	$user2->comment = "comment2";
	$user2->debt = 22.1;
	$user2->iD = 4;
	$user2->state = "inactive";
	$user2->username = "4 user";
	$dataset[2] = $user2;*/

	$result = view_all_users();
	$dataset = $result->dataSet;

	?>
    
    <h2> All User </h2>
    
    <span style="color:darkred"><?php print $status; ?></span>
    
    <form action="index.php?site=edituser" method="post">
	<table class="table table-striped table-bordered">
        <tbody><tr><th>#</th><th>Username</th><th>Debt</th><th>State</th><th>Comment</th><th>Delete?</th></tr>
                
	<?php
                
                


	foreach ($dataset as $user)
	{
		?>
		<tr>
            <td>
            	<span>
					<?php echo $user->iD; ?>
                    <input type="hidden" name="ID_<?php print $user->iD; ?>" value="<?php print $user->iD; ?>" />
                </span>
             </td>
            <td>
            	<span>
					<input type="text" name="username_new_<?php print $user->iD; ?>" value="<?php print $user->username; ?>" />
                    <input type="hidden" name="username_<?php print $user->iD; ?>" value="<?php print $user->username; ?>" />
                </span>
            </td>
            <td>
            	<span>
                	<input type="text" style="width: 50px" name="debt_new_<?php print $user->iD; ?>" value="<?php print $user->debt; ?>" />
	                <input type="hidden" name="debt_<?php print $user->iD; ?>" value="<?php print $user->debt; ?>" />
                </span>
            </td>
            <td>
            	<span>
                	<input type="text" style="width: 100px" name="state_new_<?php print $user->iD; ?>" value="<?php print $user->state; ?>" />
	                <input type="hidden" name="state_<?php print $user->iD; ?>" value="<?php print $user->state; ?>" />
                </span>
            </td>
            <td>
            	<span>
                	<input type="text" name="comment_new_<?php print $user->iD; ?>" value="<?php print $user->comment; ?>" />
	                <input type="hidden" name="comment_<?php print $user->iD; ?>" value="<?php print $user->comment; ?>" />
                </span>
            </td>
            <td>
            	<span>
                	<input type="checkbox" name="Delete_<?php print $user->iD; ?>" value="true" />
                </span>
            </td>
        </tr>      
        <?php  
	}
	
	?>
    </tbody></table>
    
    <!-- Button -->
    <div class="control-group">
      <label class="control-label" for="singlebutton"></label>
      <div class="controls">
        <button id="singlebutton" name="singlebutton" class="btn btn-primary">Confirm</button>
      </div>
    </div>
    
    </form>
    <?php
}


function vm_edit_users_check()
{
	$IDs = array();
	$Delete_IDs = array();
	$dirty_IDs = array();
	// Get all IDs from Form
	foreach ($_POST as $index => $value)
	{
		if (startsWith($index,"ID_"))
		{
			$IDs[] = str_replace("ID_","",$index);
		}
		elseif (startsWith($index,"Delete_"))
		{
			$Delete_IDs[] = str_replace("Delete_","",$index);
		}
	}
		
	// Now check for dirty elements
	foreach ($IDs as $id)
	{
		if (($_POST['username_'.$id] != $_POST['username_new_'.$id])
			|| ($_POST['debt_'.$id] != $_POST['debt_new_'.$id])
			|| ($_POST['state_'.$id] != $_POST['state_new_'.$id])
			|| ($_POST['comment_'.$id] != $_POST['comment_new_'.$id]))
		{
				// This is a dirty ID
				$dirty_IDs[] = $id;
		}
	}
	
	$edited_users = array();
	
	// create User Objects for edited users
	foreach ($dirty_IDs as $id)
	{
		$user = new User;
		$user->comment = $_POST['comment_new_'.$id];
		$user->username = $_POST['username_new_'.$id];
		$user->debt = floatval(str_replace(",",".", $_POST['debt_new_'.$id]));
		$user->state = $_POST['state_new_'.$id];
		$user->iD = $id;
		$edited_users[] = $user;
	}

	if (count($edited_users) > 0)
		$response = update_users($edited_users);
		
	if (count($Delete_IDs) > 0)
		$response2 = delete_users($Delete_IDs);
	
	$resp;
	if ((isset($response)) && (!isset($response2)))
		$resp = $response;
	elseif ((isset($response2)) && (!isset($response)))
		$resp = $response2;
	elseif ((isset($response2)) && (isset($response)))
	{
		$resp = $response;
		$resp->status &= $response2->status;
		$resp->errorDescription = $resp->errorDescription." ".$response2->errorDescription;
	}
	else
	{
		return "";
	}
	
	return $resp;
		
	
}


class Product
{
	public $iD;
	public $product;
	public $price;
	public $comment;
}

function vm_edit_products_form($status="")
{
	$result = view_all_products();
	$dataset = $result->dataSet;

	?>
    
    <h2> All Products </h2>
    
    <span style="color:darkred"><?php print $status; ?></span>
    
    <form action="index.php?site=editproducts" method="post">
	<table class="table table-striped table-bordered">
        <tbody><tr><th>#</th><th>Product</th><th>Price</th><th>Comment</th><th>Delete?</th></tr>
                
	<?php
                
                


	foreach ($dataset as $user)
	{
		?>
		<tr>
            <td>
            	<span>
					<?php echo $user->iD; ?>
                    <input type="hidden" name="ID_<?php print $user->iD; ?>" value="<?php print $user->iD; ?>" />
                </span>
             </td>
            <td>
            	<span>
					<input type="text" name="product_new_<?php print $user->iD; ?>" value="<?php print $user->product; ?>" />
                    <input type="hidden" name="product_<?php print $user->iD; ?>" value="<?php print $user->product; ?>" />
                </span>
            </td>
            <td>
            	<span>
                	<input type="text" style="width: 50px" name="price_new_<?php print $user->iD; ?>" value="<?php print $user->price; ?>" />
	                <input type="hidden" name="price_<?php print $user->iD; ?>" value="<?php print $user->price; ?>" />
                </span>
            </td>
            <td>
            	<span>
                	<input type="text" name="comment_new_<?php print $user->iD; ?>" value="<?php print $user->comment; ?>" />
	                <input type="hidden" name="comment_<?php print $user->iD; ?>" value="<?php print $user->comment; ?>" />
                </span>
            </td>
            <td>
            	<span>
                	<input type="checkbox" name="Delete_<?php print $user->iD; ?>" value="true" />
                </span>
            </td>
        </tr>      
        <?php  
	}
	
	?>
    </tbody></table>
    
    <!-- Button -->
    <div class="control-group">
      <label class="control-label" for="singlebutton"></label>
      <div class="controls">
        <button id="singlebutton" name="singlebutton" class="btn btn-primary">Confirm</button>
      </div>
    </div>
    
    </form>
    <?php
}


function vm_edit_products_check()
{
	$IDs = array();
	$Delete_IDs = array();
	$dirty_IDs = array();
	// Get all IDs from Form
	foreach ($_POST as $index => $value)
	{
		if (startsWith($index,"ID_"))
		{
			$IDs[] = str_replace("ID_","",$index);
		}
		elseif (startsWith($index,"Delete_"))
		{
			$Delete_IDs[] = str_replace("Delete_","",$index);
		}
	}
		
	// Now check for dirty elements
	foreach ($IDs as $id)
	{
		if (($_POST['price_'.$id] != $_POST['price_new_'.$id])
			|| ($_POST['product_'.$id] != $_POST['product_new_'.$id])
			|| ($_POST['comment_'.$id] != $_POST['comment_new_'.$id]))
		{
				// This is a dirty ID
				$dirty_IDs[] = $id;
		}
	}
	
	$edited_users = array();
	
	// create User Objects for edited users
	foreach ($dirty_IDs as $id)
	{
		$user = new Product;
		$user->comment = $_POST['comment_new_'.$id];
		$user->product = $_POST['product_new_'.$id];
		$user->price = floatval(str_replace(",",".", $_POST['price_new_'.$id]));
		$user->iD = $id;
		$edited_users[] = $user;
	}

	if (count($edited_users) > 0)
		$response = update_products($edited_users);
		
	if (count($Delete_IDs) > 0)
		$response2 = delete_products($Delete_IDs);
	
	$resp;
	if ((isset($response)) && (!isset($response2)))
		$resp = $response;
	elseif ((isset($response2)) && (!isset($response)))
		$resp = $response2;
	elseif ((isset($response2)) && (isset($response)))
	{
		$resp = $response;
		$resp->status &= $response2->status;
		$resp->errorDescription = $resp->errorDescription." ".$response2->errorDescription;
	}
	else
	{
		return "";
	}
	
	return $resp;
		
	
}


class Mail
{
	public $username;
	public $adress;
	public $active;
}

function vm_edit_mails_form($status="")
{
	$result = view_mails();
	$dataset = $result->dataSet;

	?>
    
    <h2> All Mails </h2>
    
    <span style="color:darkred"><?php print $status; ?></span>
    
    <form action="index.php?site=editmails" method="post">
	<table class="table table-striped table-bordered">
        <tbody><tr><th>User</th><th>Address</th><th>Delete?</th></tr>
                
	<?php
                
                


	foreach ($dataset as $user)
	{
		?>
		<tr>
            <td>
            	<span>
					<?php echo $user->username; ?>
                    <input type="hidden" name="ID_<?php print $user->username; ?>" value="<?php print $user->username; ?>" />
                </span>
             </td>
            <td>
            	<span>
					<input type="text" name="adress_new_<?php print $user->username; ?>" value="<?php print $user->adress; ?>" style="width:350px" />
                    <input type="hidden" name="adress_<?php print $user->username; ?>" value="<?php print $user->adress; ?>" />
                </span>
            </td>
            <td>
            	<span>
                	<input type="checkbox" name="Delete_<?php print $user->username; ?>" value="true" />
                </span>
            </td>
        </tr>      
        <?php  
	}
	
	?>
    </tbody></table>
    
    <!-- Button -->
    <div class="control-group">
      <label class="control-label" for="singlebutton"></label>
      <div class="controls">
        <button id="singlebutton" name="singlebutton" class="btn btn-primary">Confirm</button>
      </div>
    </div>
    
    </form>
    <?php
}


function vm_edit_mails_check()
{

	$IDs = array();
	$Delete_IDs = array();
	$dirty_IDs = array();
	// Get all IDs from Form
	foreach ($_POST as $index => $value)
	{
		if (startsWith($index,"ID_"))
		{
			$IDs[] = str_replace("ID_","",$index);
		}
		elseif (startsWith($index,"Delete_"))
		{
			$Delete_IDs[] = str_replace("Delete_","",$index);
		}
	}
		
	// Now check for dirty elements
	foreach ($IDs as $id)
	{
		if (($_POST['adress_'.$id] != $_POST['adress_new_'.$id]))
		{
				// This is a dirty ID
				$dirty_IDs[] = $id;
		}
	}
	
	$edited_users = array();
	
	// create User Objects for edited users
	foreach ($dirty_IDs as $id)
	{
		$user = new Mail;
		$user->username = $_POST['ID_'.$id];
		$user->adress = $_POST['adress_new_'.$id];
		$user->active = true;
		$edited_users[] = $user;
	}

	if (count($edited_users) > 0)
		$response = update_mails($edited_users);
		
	if (count($Delete_IDs) > 0)
		$response2 = delete_mails($Delete_IDs);
	
	$resp;
	if ((isset($response)) && (!isset($response2)))
		$resp = $response;
	elseif ((isset($response2)) && (!isset($response)))
		$resp = $response2;
	elseif ((isset($response2)) && (isset($response)))
	{
		$resp = $response;
		$resp->status &= $response2->status;
		$resp->errorDescription = $resp->errorDescription." ".$response2->errorDescription;
	}
	else
	{
		return "";
	}
	
	return $resp;
		
	
}

?>