Desenvolva o módulo finaceiro da empresa Coisas & Coisas, com as seguintes funcionalidades


| Descrição | Status |
| ------------ | ------------ |
| Manter as movimentações financeiras de contas a pagar seguindo os mesmos critérios, para considerando que o fluxo de capitais será invertido, passando a compor as despesas das empresa | TODO |
| Manter dados relativos a contas a receber | DONE |
| Quando um cliente da Coisas & Coisas e compra um ou mais produtos, é gerado um título de contas a receber (associado com a compra em questão) | DONE |
| Deve-se implementar uma rotina de movimentações sobre os títulos, registrando todas as transições que sofridas pelos títulos. Por exemplo, na criação do título, geração um registro de "Abertura; Ao receber algum valor, gera-se um registro de "Pagamento". O cancelamento também 'é um movimento | DONE |
| Os títulos de contas a receber podem assumir as seguintes situações {Aberto, Liquidado, Cancelado} | DONE |
| Ao ser criado, todo título está na situação "Aberto" e possui um movimento de abertura | DONE |
| Um título aberto pode ser Liquidado (parcial, por pagamento ou por subtituição) ou Cancelado | DONE |
| Um títulos liquidado não pode ser alterado | DONE |
| É imprescindível que cada movimento seja acompanhado de um código de transação, que determinará como movimento será integrado com o módulo contábil (plano de contas) | DONE |
| Liquidação parcial: trata-se de receber valores menores que o valor aberto do título. Quando um valor parcial é recebido, o valor aberto do títúlo é abatido e o título permanece aberto até que todo o valor aberto seja pago | DONE |
| Um titulo pode ser liquidado por subtituição: Por exemplo, um título é aberto para a compra em questão, então o cliente decide por pagar com cartão de crédito. Neste momento o títulos é sunstituido por outro título, cujo sacado passa a ser a operadora de cartão de crédito. Essa movimentação também precisa ser mapeada de forma a integrar os valor contabilmente | DONE |