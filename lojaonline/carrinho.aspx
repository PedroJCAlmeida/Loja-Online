﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="carrinho.aspx.cs" Inherits="lojaonline.carrinho" ValidateRequest="false" %>

<!DOCTYPE html>

<html lang="pt-pt" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Jéssica Almeida</title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800" rel="stylesheet"/>

    <link rel="stylesheet" href="css/open-iconic-bootstrap.min.css"/>
    <link rel="stylesheet" href="css/animate.css"/>
    
    <link rel="stylesheet" href="css/owl.carousel.min.css"/>
    <link rel="stylesheet" href="css/owl.theme.default.min.css"/>
    <link rel="stylesheet" href="css/magnific-popup.css"/>

    <link rel="stylesheet" href="css/aos.css"/>

    <link rel="stylesheet" href="css/ionicons.min.css"/>

    <link rel="stylesheet" href="css/bootstrap-datepicker.css"/>
    <link rel="stylesheet" href="css/jquery.timepicker.css"/>

    
    <link rel="stylesheet" href="css/flaticon.css"/>
    <link rel="stylesheet" href="css/icomoon.css"/>
    <link rel="stylesheet" href="css/style.css"/>
</head>
<body class="goto-here">
    <form id="form1" runat="server">
       <div class="py-1 bg-black">
    	<div class="container">
    		<div class="row no-gutters d-flex align-items-start align-items-center px-md-0">
	    		<div class="col-lg-12 d-block">
		    		<div class="row d-flex">
		    			<div class="col-md pr-4 d-flex topper align-items-center">
					    	<div class="icon mr-2 d-flex justify-content-center align-items-center"><span class="icon-phone2"></span></div>
						    <span class="text">+351 911 837 844</span>
					    </div>
					    <div class="col-md pr-4 d-flex topper align-items-right">
					    	<div class="icon mr-2 d-flex justify-content-center align-items-center"><span class="icon-paper-plane"></span></div>
						    <span class="text">jessicansalmeida2@gmail.com</span>
					    </div>
					    <div class="col-md-5 pr-4 d-flex topper align-items-center text-lg-right">
						    <span class="text">Entrega do serviço entre 3-5 dias.</span>
					    </div>
				    </div>
			    </div>
		    </div>
		  </div>
    </div>
    <nav class="navbar navbar-expand-lg navbar-dark ftco_navbar bg-dark ftco-navbar-light" id="ftco-navbar">
	    <div class="container">
	      <a class="navbar-brand" href="index.aspx">Jéssica Almeida</a>
	      <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#ftco-nav" aria-controls="ftco-nav" aria-expanded="false" aria-label="Toggle navigation">
	        <span class="oi oi-menu"></span> Menu
	      </button>

	      <div class="collapse navbar-collapse" id="ftco-nav">
	        <ul class="navbar-nav ml-auto">
	          <li class="nav-item active"><a href="index.aspx" class="nav-link">Home</a></li>
	          <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#" id="dropdown04" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Serviço</a>
              <div class="dropdown-menu" aria-labelledby="dropdown04">
              	<a class="dropdown-item" href="montra.aspx">Montra</a>
                <a class="dropdown-item" href="carrinho.aspx">Carrinho</a>
               
              </div>
            </li>
	           <li class="nav-item dropdown">
             <asp:Panel ID="Panel1" runat="server" class="nav-link dropdown-toggle" Visible="false">
                 <a  href="#" id="dropdown004" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="color:black">Gestão</a>
                <div class="dropdown-menu" aria-labelledby="dropdown04">
              	<a class="dropdown-item" href="gestao_servico.aspx">Gestão Serviço</a>
                <a class="dropdown-item" href="gestao_utilizadores.aspx">Gestão Utilizadores</a>
                <a class="dropdown-item" href="registar_servico.aspx">Registar Serviço</a>
                <a class="dropdown-item" href="registar_desconto.aspx">Criar Cupom Desconto</a>
                </div>                
             </asp:Panel>
             </li>
	          <li class="nav-item cta cta-colored"><a href="carrinho.aspx" class="nav-link"><span class="icon-shopping_cart"></span>[<asp:Label ID="lbl_itens_carrinho" runat="server" Text="0"></asp:Label>]</a></li>
              <li class="nav-item cta cta-colored"><a href="login.aspx" class="nav-link"><span class="icon-user-circle-o"></span><asp:Label ID="lbl_utilizador" runat="server" Text="Utilizador" ForeColor="Black"></asp:Label></a></li>
              <li class="nav-item cta cta-colored"><a href="logout.aspx" class="nav-link"><span class="icon-exit_to_app"></span><asp:Label ID="lbl_logout" runat="server" Text="SAIR" ForeColor="Black"></asp:Label></a></li>
              </ul>
	      </div>
	    </div>
	  </nav>
    <!-- END nav -->

    <div class="hero-wrap hero-bread" style="background-image: url('images/bg_6.jpg');">
      <div class="container">
        <div class="row no-gutters slider-text align-items-center justify-content-center">
          <div class="col-md-9 ftco-animate text-center">
          	<p class="breadcrumbs"><span class="mr-2"><a href="index.aspx">Home</a></span> <span>Carrinho</span></p>
            <h1 class="mb-0 bread">Minha Lista</h1>
          </div>
        </div>
      </div>
    </div>
        

    <section class="ftco-section bg-light">
        
           <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1" OnItemDataBound="Repeater1_ItemDataBound1" OnItemCommand="Repeater1_ItemCommand1" >
               <AlternatingItemTemplate>
                   <tbody>
                       <tr class="text-center">
                           <td class="product-remove"><a href="#">
                               <asp:Button ID="btn_eliminar" runat="server" class="product-remove" CommandName="btn_eliminar" style="opacity:0" Text="" />
                               <span class="ion-ios-close"></span></a></td>
                           <asp:Label ID="lbl_cod" runat="server" Text="" Visible="false"></asp:Label>
                           <td class="image-prod">
                               <asp:Image ID="img_servico" runat="server" class="img" Height="100px" Width="100px" />
                           </td>
                           <td class="product-name">
                               <h3>
                                   <asp:Label ID="lbl_nome_servico" runat="server" Text=""></asp:Label>
                                   </a></h3>
                               </h3>
                           </td>
                           <td class="price">€
                               <asp:Label ID="lbl_valor" runat="server" Text=""></asp:Label>
                           </td>
                           <td class="quantity">
                               <div class="input-group mb-3">
                                   <asp:Button ID="btn_diminuir" runat="server" CommandName="btn_diminuir" Text="-" />
                                   <asp:Label ID="lbl_quantidade" runat="server" class="quantity form-control input-number" style="font-size:20px" Text=""></asp:Label>
                                   <asp:Button ID="btn_adicionar" runat="server" CommandName="btn_adicionar" Text="+" />
                               </div>
                           </td>
                           <td class="total">€
                               <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label>
                           </td>
                       </tr>
                       <!-- END TR-->
                   </tbody>
               </AlternatingItemTemplate>
               <FooterTemplate>
                   </table>
                   </div>
                   </div>
                   </div>
               </FooterTemplate>
               <HeaderTemplate>
                  <section class="ftco-section ftco-cart">
			<div class="container">
				<div class="row">
    			<div class="col-md-12 ftco-animate">
    				<div class="cart-list">
	    				<table class="table">
						    <thead class="thead-primary">
						      <tr class="text-center">
						        <th>Eliminar</th>
						        <th>Foto</th>
						        <th>Serviço</th>
						        <th>Preço</th>
						        <th>Quantidade</th>
						        <th>Total</th>
						      </tr>
						    </thead>
	
                </HeaderTemplate>
                <ItemTemplate>
                 <tbody>
						      <tr class="text-center">
						        <td class="product-remove">
                                    <a href="#">
                                        <asp:Button class="product-remove" ID="btn_eliminar" runat="server" Text="" style="opacity:0" CommandName="btn_eliminar"/><span class="ion-ios-close"></span></a></td>
						        <asp:Label ID="lbl_cod" runat="server" Text="" Visible="false"></asp:Label>
						        <td class="image-prod"><asp:Image ID="img_servico" runat="server" class="img" Height="100px" Width="100px"/></td>
						        
						        <td class="product-name">
						        	<h3><asp:Label ID="lbl_nome_servico" runat="server" Text=""></asp:Label></a></h3></h3>
						        	
						        </td>
						        
						        <td class="price">€ <asp:Label ID="lbl_valor" runat="server" Text=""></asp:Label></td>
						        
						        <td class="quantity">
						        	<div class="input-group mb-3">
                                       <asp:Button  ID="btn_diminuir" runat="server" Text="-" CommandName="btn_diminuir"/><asp:Label class="quantity form-control input-number" ID="lbl_quantidade" runat="server" Text="" style="font-size:20px"></asp:Label><asp:Button ID="btn_adicionar" runat="server" Text="+" CommandName="btn_adicionar"/>
					             	</div>
					          </td>
						        
						        <td class="total">€ <asp:Label ID="lbl_total" runat="server" Text=""></asp:Label></td>
						      </tr><!-- END TR-->
                            </tbody>
                </ItemTemplate>
            </asp:Repeater>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:lojaonlineConnectionString %>" SelectCommand="SELECT carrinho.id_utilizador, carrinho.id_produto, produtos.nome_produto, carrinho.quantidade, carrinho.valor, produtos.foto, carrinho.quantidade * carrinho.valor AS total FROM carrinho INNER JOIN produtos ON carrinho.id_produto = produtos.id_produto where carrinho.id_utilizador=Null"></asp:SqlDataSource>

        </section>
         <!--Falta fazer a parte do total e ajustar o botão de diminuir para não permitir abaixo de 0 na quantiade -->
    		<div class="row justify-content-center">
                <div class="col col-lg-5 col-md-6 mt-5 cart-wrap ftco-animate">
                 	<div class="cart-total mb-3">
    					<h3>Total Carrinho</h3>
    					<p class="d-flex">
    						<span>Subtotal</span>
    						€ &nbsp<asp:Label ID="lbl_subtotal" runat="server" Text="0"></asp:Label>
    					</p>
    					<p class="d-flex">
    						<span>Desconto</span>
    						€ &nbsp<asp:Label ID="lbl_desconto" runat="server" Text="0"></asp:Label>

    					</p>
    					
    					<p class="d-flex total-price">
    						<span>Total</span>
    						€ &nbsp<asp:Label ID="lbl_total" runat="server" Text="0"></asp:Label>
    					</p>
                        <p>
                         <b>Cupom Desconto: </b><asp:TextBox ID="tb_cupom_desconto" runat="server" Width="120px"></asp:TextBox> 
                         <asp:Button ID="btn_verificar_cupom" runat="server" Text="Inserir Cupom" Width="120px" Height="50px" class="btn btn-primary py-1 px-1" OnClick="btn_verificar_cupom_Click" />
                        </p>
    				</div>
    				<p class="text-center"><asp:Button class="btn btn-primary py-3 px-4" ID="btn_finalizar_compra" runat="server" Text="Finalizar Compra" OnClick="btn_finalizar_compra_Click"/></p>
    			</div>
                

    		</div>
			
           

    	
		    	
                <br />
                <br />
                <br />
                <br />
    	
		   	
	
        <!--Footer-->
    <footer class="ftco-footer ftco-section">
      <div class="container">
      	<div class="row">
      		<div class="mouse">
						<a href="#" class="mouse-icon">
							<div class="mouse-wheel"><span class="ion-ios-arrow-up"></span></div>
						</a>
					</div>
      	</div>
        <div class="row mb-5">
          <div class="col-md">
            <div class="ftco-footer-widget mb-4">
              <h2 class="ftco-heading-2">Jéssica Almeida</h2>
              <p>Especialista em Turismo e Marketing Digital.</p>
              <ul class="ftco-footer-social list-unstyled float-md-left float-lft mt-5">
                <li class ="ftco-animate"><a href="https://www.facebook.com/jessicansalmeida" target="_blank"><span class="icon-facebook"></span></a></li>
                <li class="ftco-animate"><a href="https://www.instagram.com/jessicansalmeida/" target="_blank"><span class="icon-instagram"></span></a></li>
              </ul>
            </div>
          </div>
          <div class="col-md">
            <div class="ftco-footer-widget mb-4 ml-md-5">
              <h2 class="ftco-heading-2">Menu</h2>
              <ul class="list-unstyled">
                <li><a href="montra.aspx" class="py-2 d-block">Montra</a></li>
                <li><a href="carrinho.aspx" class="py-2 d-block">Carrinho</a></li>
                
              </ul>
            </div>
          </div>
          
          <div class="col-md">
            <div class="ftco-footer-widget mb-4">
            	<h2 class="ftco-heading-2">Contactos</h2>
            	<div class="block-23 mb-3">
	              <ul>
	                <li><span class="icon icon-map-marker"></span><span class="text">Vila Nova de Gaia, Porto, Portugal</span></li>
	                <li><a href="tel:+351911837844"><span class="icon icon-phone"></span><span class="text">+351 911837844</span></a></li>
	                <li><a href="https://wa.me/351911837844" ><span class="icon icon-whatsapp"></span><span class="text">+351 911837844</span></a></li>
                    <li><a href="mailto:jessicansalmeida2@gmail.com?"><span class="icon icon-envelope"></span><span class="text">jessicansalmeida2@gmail.com</span></a></li>
	              </ul>
	            </div>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-md-12 text-center">

            <p><!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. -->
						  Copyright &copy;<script>document.write(new Date().getFullYear());</script> All rights reserved | This template is made with <i class="icon-heart color-danger" aria-hidden="true"></i> by <a href="https://colorlib.com" target="_blank">Colorlib</a>
						  <!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. -->
						</p>
          </div>
        </div>
      </div>
    </footer>
    
  

  <!-- loader -->
  <div id="ftco-loader" class="show fullscreen"><svg class="circular" width="48px" height="48px"><circle class="path-bg" cx="24" cy="24" r="22" fill="none" stroke-width="4" stroke="#eeeeee"/><circle class="path" cx="24" cy="24" r="22" fill="none" stroke-width="4" stroke-miterlimit="10" stroke="#F96D00"/></svg></div>


  <script src="js/jquery.min.js"></script>
  <script src="js/jquery-migrate-3.0.1.min.js"></script>
  <script src="js/popper.min.js"></script>
  <script src="js/bootstrap.min.js"></script>
  <script src="js/jquery.easing.1.3.js"></script>
  <script src="js/jquery.waypoints.min.js"></script>
  <script src="js/jquery.stellar.min.js"></script>
  <script src="js/owl.carousel.min.js"></script>
  <script src="js/jquery.magnific-popup.min.js"></script>
  <script src="js/aos.js"></script>
  <script src="js/jquery.animateNumber.min.js"></script>
  <script src="js/bootstrap-datepicker.js"></script>
  <script src="js/scrollax.min.js"></script>
  <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBVWaKrjvy3MaE7SQ74_uJiULgl1JY0H2s&sensor=false"></script>
  <script src="js/google-map.js"></script>
  <script src="js/main.js"></script>
    
  </form>
</body>
</html>
