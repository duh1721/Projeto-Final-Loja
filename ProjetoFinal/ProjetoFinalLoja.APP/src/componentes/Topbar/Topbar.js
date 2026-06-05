import style from "./Topbar.module.css";
import { Link, useNavigate } from "react-router-dom";
import { MdLogout } from "react-icons/md";

export function Topbar({ titulo, children }) {
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("usuarioLogado");
        navigate("/login");
    };

    return (
        <div>
            <div className={style.topbar_conteudo}>
                <span className={style.topbar_titulo}>{titulo}</span>
                <div className={style.topbar_acoes}>
                    <button
                        onClick={handleLogout}
                        className={style.botao_deslogar}
                        style={{ background: "none", border: "none", cursor: "pointer" }}
                        title="Sair"
                    >
                        <MdLogout />
                    </button>
                </div>
            </div>
            <div className={style.pagina_conteudo}>
                {children}
            </div>
        </div>
    );
}
