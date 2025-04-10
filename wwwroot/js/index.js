const botao = document.querySelector('.btn');
const input = document.querySelector('input');
const tBody = document.querySelector('tbody');
const listaDeTelefones = [];

input.addEventListener('input', () => {
  console.log(input.value)
  input.value = input.value.replace(/\D/g, "");
})

function validaTelefone() {
  const cPai = document.querySelector('.containerPai');
  const mensagensAnteriores = cPai.querySelectorAll('.mensagem-erro');
  mensagensAnteriores.forEach(m => m.remove());
  if (input.value.length < 10) {
    const p = document.createElement('p');
    p.textContent = "Mínimo de 10 dígitos";
    p.classList.add('bg-danger', 'text-light', 'mt-2', 'mensagem-erro');
    cPai.appendChild(p);
    setInterval(() => {
      p.remove();
    }, 2000);
    return false;
  }
  return true;
}

function formataTelefone(str) {
  return str
    .replace(/\D/g, '')
    .replace(/(?:(^\+\d{2})?)(?:([1-9]{2})|([0-9]{3})?)(\d{4,5})(\d{4})/,
        (match, pais, ddd, dddSemZero, prefixo, sufixo) => {
            if (pais) return `${ pais }${ ddd || dddSemZero } ${ prefixo }${ sufixo}`;
            if (ddd || dddSemZero) return `${ ddd || dddSemZero }${ prefixo }${ sufixo}`;
            if (prefixo && sufixo) return `${ prefixo }${ sufixo }`;
            return str;
        }
    );
}

function adicionarTelefone(e) {
  e.preventDefault();
  const telefone = input.value.trim();
  const telefoneFormatado = formataTelefone(telefone);

  if(!validaTelefone()){
    return;
  }

  if(telefoneFormatado != ""){
    const td = document.createElement('td');
    const tr = document.createElement('tr');

    tr.appendChild(td);
    td.textContent = telefoneFormatado;
    td.classList.add('telefone-tabela');
    tBody.appendChild(tr);
  }
}

botao.addEventListener('click', adicionarTelefone)