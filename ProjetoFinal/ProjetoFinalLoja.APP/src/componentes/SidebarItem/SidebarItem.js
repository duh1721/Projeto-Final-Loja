import style from './SidebarItem.module.css';
import { NavLink } from 'react-router-dom';

export function SidebarItem({ texto, link, logo }) {
    return (
        <NavLink
            to={link}
            className={({ isActive }) =>
                isActive
                    ? `${style.sidebar_item} ${style.ativo}`
                    : style.sidebar_item
            }
        >
            <span className={style.icone}>{logo}</span>
            <h3 className={style.texto_link}>{texto}</h3>
        </NavLink>
    );
}
