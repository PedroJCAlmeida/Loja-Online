<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="/">
            <a class="nav-link dropdown-toggle" href="#" id="dropdown004" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Gestão</a>
              <div class="dropdown-menu" aria-labelledby="dropdown04">
              	<a class="dropdown-item" href="gestao_servico.aspx">Gestão Serviço</a>
                <a class="dropdown-item" href="gestao_utilizadores.apsx">Gestão Utilizadores</a>
                <a class="dropdown-item" href="registar_servico.aspx">Registar Serviço</a>
                </div>
    </xsl:template>
</xsl:stylesheet>
