
# Project ItiDigital 
Projeto desenvolvido com o objetivo de validar uma Senha, com alguns pre-requisitos
1- Nove ou mais caracteres
2- Ao menos 1 dígito
3- Ao menos 1 letra minúscula
4- Ao menos 1 letra maiúscula
5- Ao menos 1 caractere especial
5.1- Considere como especial os seguintes caracteres: !@#$%^&*()-+
6- Não possuir caracteres repetidos dentro do conjunto

# Build project
Abrir Powershell na pasta da Solution executar o comando abaixo.
--> docker-compose -f docker-compose.build.yaml up --build

Aplicação ficara no endereço: http://localhost:5050
Documentação da Aplicação: http://localhost:5050/Swagger

Usei essa porta para não impactar caso tenha algum outro projeto rodando, então direcionei a porta para 5050.

# EndPoint
Foi criação o EndPoint na rota: /Validation/Password para chamada de validação de senha com um contrato pré-determinado.
O contrato tem o seguinte Schema: 
{
	"password": "string"
}

# Validation 
Ao iniciar o projeto pensei em uma classe Estática como método de extensão para validar os requisitos da senha, para que possa ser reutilizado
	em outros processos caso tenha necessidade e não ficar restrita a camada de serviço que tem como responsabilidade somente processar
	e responder a solicitação, o método de extensão fica com a interpretação do dados vindo.

# Documentation API
No Swagger está documentado o contrato de comunicação e resposta, levando em consideração os possíveis retornos.

# Unit Test
Foi comtemplado nos testes unitários os pontos de validação e falha.

# Integration Test
Foi criado uma requisição através do HttpClientFactory para validação dos possíveis resultados da API, levando em consideração a regra de negócio do EndPoint.

# Logging Project
O Projeto já está configurado para geração de Log através do CloudWatch com a Biblioteca Serilog.

#Envroviment
O Projeto já comtempla a substituição das variáveis do AppSettings pelas variáveis de ambiente no Program da Aplicação.

#Challeng Decision
Fiz uso do Regex para optimização dos padrões de validação já que é uma biblioteca apropriada para este uso.
