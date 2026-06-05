import style from "./Sidebar.module.css";
import { SidebarItem } from "../SidebarItem/SidebarItem";
import { MdDashboard, MdInventory2, MdPeople, MdShoppingCart, MdSmartToy } from "react-icons/md";
import { Link } from "react-router-dom";

export function Sidebar({ children }) {
    return (
        <div>
            <div className={style.sidebar_conteudo}>
                <div className={style.sidebar_header}>
                    <Link to="/dashboard" style={{ textDecoration: "none" }}>
                        <div className={style.titulo}>ShopMind</div>
                        <div className={style.subtitulo}>Admin Panel</div>
                    </Link>
                    <hr className={style.linha} />
                </div>

                <div className={style.sidebar_corpo}>
                    <SidebarItem texto="Dashboard"    link="/dashboard"     logo={<MdDashboard />} />
                    <SidebarItem texto="Produtos"     link="/produtos"      logo={<MdInventory2 />} />
                    <SidebarItem texto="Clientes"     link="/clientes"      logo={<MdPeople />} />
                    <SidebarItem texto="Pedidos"      link="/pedidos"       logo={<MdShoppingCart />} />
                    <SidebarItem texto="Assistente IA" link="/assistente-ia" logo={<MdSmartToy />} />
                </div>

            </div>

            <div className={style.pagina_conteudo}>
                {children}
            </div>
        </div>
    );
}
