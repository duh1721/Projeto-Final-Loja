import { Sidebar } from "../../componentes/Sidebar/Sidebar";
import { Topbar } from "../../componentes/Topbar/Topbar";
import { useState, useRef, useEffect } from "react";
import IAAPI from "../../services/IAAPI";
import style from "./AssistenteIA.module.css";

const USUARIO_ID = "usuario_" + Math.random().toString(36).substr(2, 8);

export function AssistenteIA() {
  const [mensagens, setMensagens] = useState([
    {
      tipo: "ia",
      texto:
        "Olá! Sou a vendedora virtual da Loja Vendas Posso ajudá-lo a fazer pedidos, verificar produtos e muito mais. Como posso te ajudar?",
    },
  ]);
  const [input, setInput] = useState("");
  const [carregando, setCarregando] = useState(false);
  const messagesEndRef = useRef(null);

  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [mensagens, carregando]);

  const enviar = async () => {
    const texto = input.trim();
    if (!texto || carregando) return;

    setMensagens((prev) => [...prev, { tipo: "user", texto }]);
    setInput("");
    setCarregando(true);

    try {
      const resposta = await IAAPI.perguntarAsync(texto, USUARIO_ID);
      const textoResposta =
        typeof resposta === "string"
          ? resposta.replace(/^"|"$/g, "")
          : JSON.stringify(resposta);
      setMensagens((prev) => [...prev, { tipo: "ia", texto: textoResposta }]);
    } catch (error) {
      setMensagens((prev) => [
        ...prev,
        {
          tipo: "ia",
          texto:
            "Não consegui me conectar à API. Verifique se o servidor está rodando.",
        },
      ]);
      console.error(error);
    } finally {
      setCarregando(false);
    }
  };

  const handleKeyDown = (e) => {
    if (e.key === "Enter" && !e.shiftKey) {
      e.preventDefault();
      enviar();
    }
  };

  return (
    <Sidebar>
      <Topbar titulo="Assistente IA">
        <div className={style.pagina_conteudo}>
          <div className={style.cabecalho}>
            <h3>Assistente IA</h3>
            <p>
              Converse com a vendedora virtual para fazer pedidos e tirar
              dúvidas
            </p>
          </div>

          <div className={style.chat_wrap}>
            <div className={style.chat_header}>
              <div className={style.chat_avatar}>🤖</div>
              <div className={style.chat_info}>
                <div className={style.nome}>Vendedora Virtual</div>
                <div className={style.status}>● Online</div>
              </div>
            </div>

            <div className={style.chat_mensagens}>
              {mensagens.map((msg, i) => (
                <div
                  key={i}
                  className={
                    msg.tipo === "ia" ? style.msg_wrap_ia : style.msg_wrap_user
                  }
                >
                  {msg.tipo === "ia" && (
                    <div className={style.msg_label}>Assistente</div>
                  )}
                  <div
                    className={
                      msg.tipo === "ia"
                        ? style.msg_ia
                        : `${style.msg} ${style.msg_user}`
                    }
                  >
                    {msg.texto}
                  </div>
                </div>
              ))}
              {carregando && (
                <div className={style.typing}>
                  <div className={style.dot}></div>
                  <div className={style.dot}></div>
                  <div className={style.dot}></div>
                </div>
              )}
              <div ref={messagesEndRef} />
            </div>

            <div className={style.chat_input_area}>
              <textarea
                className={style.chat_input}
                value={input}
                onChange={(e) => setInput(e.target.value)}
                onKeyDown={handleKeyDown}
                placeholder="Digite sua mensagem... (Enter para enviar)"
                rows={1}
              />
              <button
                className={style.botao_enviar}
                onClick={enviar}
                disabled={carregando}
              >
                {carregando ? "..." : "Enviar →"}
              </button>
            </div>
          </div>
        </div>
      </Topbar>
    </Sidebar>
  );
}
