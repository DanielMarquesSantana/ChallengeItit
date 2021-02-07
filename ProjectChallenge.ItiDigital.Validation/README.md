
# Project ItiDigital 
Projeto desenvolvido com o objetivo de validar uma Senha, com alguns pre-requisitos
1- Nove ou mais caracteres
2- Ao menos 1 d�gito
3- Ao menos 1 letra min�scula
4- Ao menos 1 letra mai�scula
5- Ao menos 1 caractere especial
5.1- Considere como especial os seguintes caracteres: !@#$%^&*()-+
6- N�o possuir caracteres repetidos dentro do conjunto

# Build project
Abrir Powershell na pasta da Solution executar o comando abaixo.
--> docker-compose -f docker-compose.build.yaml up --build

Aplica��o ficara no endere�o: http://localhost:5050
Documenta��o da Aplica��o: http://localhost:5050/Swagger

Usei essa porta para n�o impactar caso tenha algum outro projeto rodando, ent�o direcionei a porta para 5050.

# EndPoint
Foi cria��o o EndPoint na rota: /Validation/Password para chamada de valida��o de senha com um contrato pr�-determinado.
O contrato tem o seguinte Schema: 
{
	"password": "string"
}

# Validation 
Ao iniciar o projeto pensei em uma classe Est�tica como m�todo de extens�o para validar os requisitos da senha, para que possa ser reutilizado
	em outros processos caso tenha necessidade e n�o ficar restrita a camada de servi�o que tem como responsabilidade somente processar
	e responder a solicita��o, o m�todo de extens�o fica com a interpreta��o do dados vindo.

# Documentation API
No Swagger est� documentado o contrato de comunica��o e resposta, levando em considera��o os poss�veis retornos.

# Unit Test
Foi comtemplado nos testes unit�rios os pontos de valida��o e falha.

# Integration Test
Foi criado uma requisi��o atrav�s do HttpClientFactory para valida��o dos poss�veis resultados da API, levando em considera��o a regra de neg�cio do EndPoint.

# Logging Project
O Projeto j� est� configurado para gera��o de Log atrav�s do CloudWatch com a Biblioteca Serilog.

#Envroviment
O Projeto j� comtempla a substitui��o das vari�veis do AppSettings pelas vari�veis de ambiente no Program da Aplica��o.

#Challeng Decision
Fiz uso do Regex para optimiza��o dos padr�es de valida��o j� que � uma biblioteca apropriada para este uso.
