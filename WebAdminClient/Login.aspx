﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebAdminClient.Login" Async="true" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="favicon.ico">

    <title><%: WebAdminClient.StringsLocal.SignIn %></title>

    <!-- Bootstrap core CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="signin.css" rel="stylesheet">


    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
  </head>

  <body>

    <div class="container" style="min-width:100px; max-width:300px; ">

      <form class="form-signin" runat="server">
        <h2 class="form-signin-heading"><%: WebAdminClient.StringsLocal.SignIn %></h2>
        <label for="inputUser" class="sr-only"><%: WebAdminClient.StringsLocal.Username %></label>
        <input type="text" id="inputUser" class="form-control" placeholder="Username" runat="server" required autofocus>
        <label for="inputPassword" class="sr-only">Password</label>
        <input type="password" id="inputPassword" class="form-control" placeholder="Password" runat="server" required>
        <div class="checkbox">
          <label>
            <input type="checkbox" value="remember-me" runat="server" id="remember"> <%: WebAdminClient.StringsLocal.LongSession %>
          </label>
        </div>
        <button class="btn btn-lg btn-primary btn-block" type="submit"><%: WebAdminClient.StringsLocal.SignIn %></button>
      </form>
        <span style="color:darkred"><%: LoginMessage %></span>

    </div> <!-- /container -->

  </body>
</html>
