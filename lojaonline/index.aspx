<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="lojaonline.index" %>

<!DOCTYPE html>

<html lang="pt-pt" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <section id="home-section" class="hero">
		  <div class="home-slider owl-carousel">
	      <div class="slider-item js-fullheight">
	      	<div class="overlay"></div>
	        <div class="container-fluid p-0">
	          <div class="row d-md-flex no-gutters slider-text align-items-center justify-content-end" data-scrollax-parent="true">
	          	<img class="one-third order-md-last img-fluid" src="images/servicos-marketing-digital.png" alt=""/>
		          <div class="one-forth d-flex align-items-center ftco-animate" data-scrollax=" properties: { translateY: '70%' }">
		          	<div class="text">
		          		    <h1 class="mb-4 mt-3">Serviços</h1> 
				            <p class="mb-4">O Lorem Ipsum é um texto modelo da indústria tipográfica e de impressão. O Lorem Ipsum tem vindo a ser o texto padrão usado por estas indústrias desde o ano de 1500.</p>
				             </div>
		             </div>
	        	</div>
	        </div>
	      </div>

	      <div class="slider-item js-fullheight">
	      	<div class="overlay"></div>
	        <div class="container-fluid p-0">
	          <div class="row d-flex no-gutters slider-text align-items-center justify-content-end" data-scrollax-parent="true">
	          	 	<img class="one-third order-md-last img-fluid" src="images/servicos-marketing-digital.png" alt=""/>
		       <div class="one-forth d-flex align-items-center ftco-animate" data-scrollax=" properties: { translateY: '70%' }">
		          	<div class="text">
		          		    <h1 class="mb-4 mt-3">Serviços</h1>
				            <p class="mb-4">O Lorem Ipsum é um texto modelo da indústria tipográfica e de impressão. O Lorem Ipsum tem vindo a ser o texto padrão usado por estas indústrias desde o ano de 1500.</p>
				             </div>
		             </div>
	        	</div>
	        </div>
	      </div>
	    </div>
    </section>

    <section class="ftco-section bg-light">
    	<div class="container">
				<div class="row justify-content-center mb-3 pb-3">
          <div class="col-md-12 heading-section text-center ftco-animate">
            <h2 class="mb-4">Principais Serviços</h2>
            <p></p>
          </div>
        </div>   		
    	</div>
    	
    			
                        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1" OnItemDataBound="Repeater1_ItemDataBound" OnItemCommand="Repeater1_ItemCommand">
                            <HeaderTemplate>
                        <div class="container">
                            <div class="row">
    				 
                            </HeaderTemplate>
                            <ItemTemplate>
                       <div class="col-sm-12 col-md-6 col-lg-3 ftco-animate d-flex">                   
                        <div class="product d-flex flex-column">
    					<asp:Label ID="lbl_cod" runat="server" Text="" Visible="false"></asp:Label>
		    		    <asp:Image ID="img_servico" runat="server" class="img-fluid"/><a href="#" class="img-prod"/>
                        <div class="overlay"></div>
    					</a>
    					<div class="text py-3 pb-4 px-3">
    						
    						<h3><a href="#"><asp:Label ID="lbl_nome_servico" runat="server" Text=""></asp:Label></a></h3>
		    					<div class="pricing">
	    						<p class="price"><span>€ <asp:Label ID="lbl_valor" runat="server" Text=""></asp:Label></span></p>
			    					</div>
	    					<p class="bottom-area d-flex px-3">
    							<a href="#" class="add-to-cart text-center py-2 mr-1"><asp:Button ID="btn_add_carrinho" runat="server" Text="Adicionar"  BackColor="Black" ForeColor="White" Width="80px" CommandName="btn_add_carrinho"></asp:Button>
		    							<a href="#" class="add-to-cart text-center py-2 mr-1"><asp:Button ID="btn_comprar" runat="server" Text="Comprar" BackColor="Black" ForeColor="White" Width="80px" CommandName="btn_comprar"></asp:Button></a>
		    					</p>
    					</div>
    				</div>
                            </div>
                               
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <div class="col-sm-12 col-md-6 col-lg-3 ftco-animate d-flex">
                               <div class="product d-flex flex-column">
    					<asp:Label ID="lbl_cod" runat="server" Text="" Visible="false"></asp:Label>
		    		    <asp:Image ID="img_servico" runat="server" class="img-fluid"/><a href="#" class="img-prod"/>
                        <div class="overlay"></div>
    					</a>
    					<div class="text py-3 pb-4 px-3">
    						
    						<h3><a href="#"><asp:Label ID="lbl_nome_servico" runat="server" Text=""></asp:Label></a></h3>
		    					<div class="pricing">
	    						<p class="price"><span>€ <asp:Label ID="lbl_valor" runat="server" Text=""></asp:Label></span></p>
			    					</div>
	    					<p class="bottom-area d-flex px-3">
    							<a href="#" class="add-to-cart text-center py-2 mr-1"><asp:Button ID="btn_add_carrinho" runat="server" Text="Adicionar"  BackColor="Black" ForeColor="White" Width="80px" CommandName="btn_add_carrinho"></asp:Button>
		    					<a href="#" class="add-to-cart text-center py-2 mr-1"><asp:Button ID="btn_comprar" runat="server" Text="Comprar" BackColor="Black" ForeColor="White" Width="80px" CommandName="btn_comprar"></asp:Button></a>
		    					</p>
    					</div>
    				</div>
                            </div>
                            
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                
    			</div>
                    </div>
                            </FooterTemplate>
                        </asp:Repeater>
                         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:lojaonlineConnectionString %>" SelectCommand="SELECT top 4 * FROM [produtos] ORDER BY [id_produto] DESC 
">
           </asp:SqlDataSource>
            </section>



    <!--Footer -->   

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
