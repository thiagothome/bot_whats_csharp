const botao = document.querySelector('.btn');
const input = document.querySelector('input');
const tBody = document.querySelector('tbody');
const mensagemInput = document.getElementById('mensagem');
const mensagemErro = document.getElementById('mensagemErro');
const btnSalvar = document.getElementById('btnSalvar');

const listaDeTelefones = [];

input.addEventListener('input', () => {
  console.log(input.value)
  input.value = input.value.replace(/\D/g, "");
})

function exibirMensagem(texto, tipo = 'erro') {
  const elementoMensagem = document.getElementById('mensagemErro');
  elementoMensagem.classList.remove('d-none');

  if (tipo === 'erro') {
    elementoMensagem.classList.remove('bg-success');
    elementoMensagem.classList.add('bg-danger');
  } else {
    elementoMensagem.classList.remove('bg-danger');
    elementoMensagem.classList.add('bg-success');
  }

  elementoMensagem.textContent = texto;

  setTimeout(() => {
    elementoMensagem.classList.add('d-none');
  }, 5000);
}

function formataTelefone(numero) {
  numero = numero.replace(/\D/g, '');
  if (numero.length === 11) {
    return `(${numero.slice(0, 2)}) ${numero.slice(2, 7)}-${numero.slice(7)}`;
  } else if (numero.length === 10) {
    return `(${numero.slice(0, 2)}) ${numero.slice(2, 6)}-${numero.slice(6)}`;
  }
  return numero;
}

function validaTelefone() {
  const numero = input.value.trim();
  if (!/^\d{10,}$/.test(numero)) {
    exibirMensagemErro("Número inválido. Insira pelo menos 10 dígitos.");
    return false;
  }
  return true;
}

function validaMensagem() {
  const mensagem = mensagemInput.value.trim();
  if (mensagem === "") {
    exibirMensagemErro("A mensagem não pode estar vazia");
    return false;
  }
  return true;
}


function Enviar() {debugger
  const telefones = [...document.querySelectorAll('.telefone-tabela')].map(td => td.textContent);
  const mensagem = mensagemInput.value.trim();

  if (telefones.length === 0) {
    exibirMensagemErro("Adicione pelo menos um número.");
    return;
  }

  if (!validaMensagem()) return;

  fetch('/Mensagem/Enviar', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({
      Telefones: telefones,
      Mensagem: mensagem
    })
  })
  .then(res => {
    if (res.ok) {
      exibirMensagem('Mensagens enviadas com sucesso!', 'sucesso');
    } else {
      exibirMensagem('Erro ao enviar mensagens', 'erro');
    }
  })
  .catch(err => {
    console.error('Erro:', err);
    exibirMensagem("Erro de conexão com o servidor.", 'erro');
  });
}  

function adicionarTelefone(e) {
  e.preventDefault();
  const telefone = input.value.trim();

  if (!validaTelefone()) return;

  const telefoneFormatado = formataTelefone(telefone);

  const telefonesAdicionados = [...document.querySelectorAll('.telefone-tabela')]
    .map(td => td.textContent);

    if (telefonesAdicionados.includes(telefoneFormatado)) {
      exibirMensagemErro('Este número já foi adicionado.');
      limparInputTelefone();
      return;
    }
    

  if (telefoneFormatado !== "") {
    const td = document.createElement('td');
    const tr = document.createElement('tr');

    td.textContent = telefoneFormatado;
    td.classList.add('telefone-tabela');

    tr.appendChild(td);
    tBody.appendChild(tr);

    input.value = "";
    input.focus();
  }
}

botao.addEventListener('click', adicionarTelefone);
document.getElementById('btnSalvar').addEventListener('click', Enviar);
