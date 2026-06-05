import style from "./Login.module.css";
import Form from "react-bootstrap/Form";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import LoginAPI from "../../services/LoginAPI";
import ClienteAPI from "../../services/ClienteAPI";

export function Login() {
  const [tela, setTela] = useState("login");

  // estados login
  const [email, setEmail] = useState("");
  const [senha, setSenha] = useState("");

  //estado de endereço
  const [rua, setRua] = useState("");
  const [numero, setNumero] = useState("");
  const [bairro, setBairro] = useState("");
  const [cidade, setCidade] = useState("");
  const [estado, setEstado] = useState("");
  const [cep, setCep] = useState("");

  // estados cadastro
  const [nome, setNome] = useState("");
  const [emailC, setEmailC] = useState("");
  const [senhaC, setSenhaC] = useState("");
  const [Telefone, setTelefone] = useState("");
  const [tipoUsuario] = useState(2);
  const [confirmarSenha, setConfirmarSenha] = useState("");

  const [erro, setErro] = useState("");
  const [sucesso, setSucesso] = useState("");
  const navigate = useNavigate();

  const trocarTela = (para) => {
    setErro("");
    setSucesso("");
    setTela(para);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErro("");

    if (!email || !senha) {
      setErro("Preencha todos os campos.");
      return;
    }

    try {
      const response = await LoginAPI.loginAsync(email, senha);

      if (response.token) {
        localStorage.setItem("token", response.token);
        localStorage.setItem(
          "usuarioLogado",
          JSON.stringify({
            id: response.id,
            nome: response.nome,
            email: response.email,
            tipoUsuario: response.tipoUsuario,
          }),
        );
        navigate("/dashboard");
      }
    } catch {
      setErro("Email ou senha inválidos.");
    }
  };

  const handleCadastro = async (e) => {
    e.preventDefault();
    setErro("");
    setSucesso("");

    if (!nome || !emailC || !senhaC || !confirmarSenha || !Telefone) {
      setErro("Preencha todos os campos.");
      return;
    }

    if (senhaC !== confirmarSenha) {
      setErro("As senhas não coincidem.");
      return;
    }

    try {
      const response = await ClienteAPI.criarAsync(
        nome,
        emailC,
        Telefone,
        tipoUsuario,
        senhaC,
      );
      const idExtraido = response.match(/\d+/)[0];

      localStorage.setItem("clienteIdTemp", idExtraido);

      setSucesso("Cadastro realizado! Agora cadastre seu endereço.");
      setTimeout(() => trocarTela("endereço"), 2000);
    } catch {
      setErro("Erro ao cadastrar. Tente novamente.");
    }
  };

  const handleEndereco = async (e) => {
    e.preventDefault();
    setErro("");
    setSucesso("");

    if (!rua || !numero || !bairro || !cidade || !estado || !cep) {
      setErro("Preencha todos os campos.");
      return;
    }

    const clienteId = Number(localStorage.getItem("clienteIdTemp"));

    try {
      await ClienteAPI.EnderecoAsync({
        Rua: rua,
        Numero: numero,
        Bairro: bairro,
        Cidade: cidade,
        Estado: estado,
        Cep: cep,
        ClienteId: clienteId
      });
      localStorage.removeItem("clienteIdTemp");
      setSucesso("Endereço cadastrado com sucesso!");
      setTimeout(() => trocarTela("login"), 1000);
    } catch {
      setErro("Erro ao cadastrar endereço. Tente novamente.");
    }
  };

  return (
    <div className={style.conteudo}>
      <div className={style.card_login}>
        {tela === "login" && (
          <>
            <div className={style.titulo}>ShopMind</div>
            {erro && <div className={style.erro}>{erro}</div>}

            <Form className={style.formulario} onSubmit={handleSubmit}>
              <Form.Group className="mb-3" controlId="formEmail">
                <Form.Label>Email</Form.Label>
                <Form.Control
                  type="email"
                  placeholder="seu@email.com"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  required
                />
              </Form.Group>

              <Form.Group className="mb-3" controlId="formSenha">
                <Form.Label>Senha</Form.Label>
                <Form.Control
                  type="password"
                  placeholder="••••••••"
                  value={senha}
                  onChange={(e) => setSenha(e.target.value)}
                  required
                />
              </Form.Group>

              <button type="submit" className={style.botao_entrar}>
                Entrar
              </button>
              <button
                type="button"
                className={style.botao_cadastrar}
                onClick={() => trocarTela("cadastro")}
              >
                Criar Conta
              </button>
            </Form>
          </>
        )}

        {tela === "cadastro" && (
          <>
            <div className={style.titulo}>Criar Conta</div>
            {erro && <div className={style.erro}>{erro}</div>}
            {sucesso && <div className={style.sucesso}>{sucesso}</div>}

            <Form className={style.formulario} onSubmit={handleCadastro}>
              <Form.Group className="mb-3">
                <Form.Label>Nome</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Seu nome completo"
                  value={nome}
                  onChange={(e) => setNome(e.target.value)}
                  required
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Email</Form.Label>
                <Form.Control
                  type="email"
                  placeholder="seu@email.com"
                  value={emailC}
                  onChange={(e) => setEmailC(e.target.value)}
                  required
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Telefone</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Seu telefone"
                  value={Telefone}
                  onChange={(e) => setTelefone(e.target.value)}
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Senha</Form.Label>
                <Form.Control
                  type="password"
                  placeholder="••••••••"
                  value={senhaC}
                  onChange={(e) => setSenhaC(e.target.value)}
                  required
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Confirmar Senha</Form.Label>
                <Form.Control
                  type="password"
                  placeholder="••••••••"
                  value={confirmarSenha}
                  onChange={(e) => setConfirmarSenha(e.target.value)}
                  required
                />
              </Form.Group>

              <button type="submit" className={style.botao_entrar}>
                Cadastrar
              </button>
              <button
                type="button"
                className={style.botao_cadastrar}
                onClick={() => trocarTela("login")}
              >
                Já tenho conta
              </button>
            </Form>
          </>
        )}

        {tela === "endereço" && (
          <>
            <div className={style.titulo}>Cadastrar Endereço</div>
            {erro && <div className={style.erro}>{erro}</div>}
            {sucesso && <div className={style.sucesso}>{sucesso}</div>}

            <Form className={style.formulario} onSubmit={handleEndereco}>
              <Form.Group className="mb-3">
                <Form.Label>Rua</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Rua"
                  value={rua}
                  onChange={(e) => setRua(e.target.value)}
                  required
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Número</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Número"
                  value={numero}
                  onChange={(e) => setNumero(e.target.value)}
                  required
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Bairro</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Bairro"
                  value={bairro}
                  onChange={(e) => setBairro(e.target.value)}
                  required
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Cidade</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Cidade"
                  value={cidade}
                  onChange={(e) => setCidade(e.target.value)}
                  required
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Estado</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Estado"
                  value={estado}
                  onChange={(e) => setEstado(e.target.value)}
                  required
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>CEP</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="CEP"
                  value={cep}
                  onChange={(e) => setCep(e.target.value)}
                  required
                />
              </Form.Group>

              <button type="submit" className={style.botao_entrar}>
                Cadastrar Endereço
              </button>
            </Form>
          </>
        )}
      </div>
    </div>
  );
}
