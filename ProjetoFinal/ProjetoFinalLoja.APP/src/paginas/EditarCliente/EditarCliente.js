import { Sidebar } from "../../componentes/Sidebar/Sidebar";
import { Topbar } from "../../componentes/Topbar/Topbar";
import { Link, useNavigate, useLocation } from "react-router-dom";
import Form from "react-bootstrap/Form";
import { useState, useEffect } from "react";
import ClienteAPI from "../../services/ClienteAPI";
import style from "./EditarCliente.module.css";
import { MdArrowBack } from "react-icons/md";

export function EditarCliente() {
    const navigate = useNavigate();
    const location = useLocation();
    const id = location.state;
    const [erro, setErro] = useState("");
    const [form, setForm] = useState({ nome: "", email: "", telefone: "", ativo: true });

    useEffect(() => {
        async function carregar() {
            try {
                const c = await ClienteAPI.obterPorIdAsync(id);
                setForm({ nome: c.nome || "", email: c.email || "", telefone: c.telefone || "", ativo: c.ativo ?? true });
            } catch (error) { console.error(error); }
        }
        if (id) carregar();
    }, [id]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setForm(prev => ({ ...prev, [name]: type === "checkbox" ? checked : value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await ClienteAPI.atualizarAsync({ id, ...form });
            navigate("/clientes");
        } catch (error) { setErro("Erro ao atualizar cliente."); console.error(error); }
    };

    return (
        <Sidebar>
            <Topbar titulo="Editar Cliente">
                <div className={style.pagina_conteudo}>
                    <div className={style.pagina_cabecalho}>
                        <Link to="/clientes" className={style.botao_voltar}><MdArrowBack /> Voltar</Link>
                        <h3>Editar Cliente</h3>
                    </div>
                    <div className={style.card_form}>
                        {erro && <div className={style.erro}>{erro}</div>}
                        <Form onSubmit={handleSubmit}>
                            <Form.Group className="mb-3">
                                <Form.Label>Nome *</Form.Label>
                                <Form.Control name="nome" value={form.nome} onChange={handleChange} required />
                            </Form.Group>
                            <Form.Group className="mb-3">
                                <Form.Label>Email</Form.Label>
                                <Form.Control type="email" name="email" value={form.email} onChange={handleChange} />
                            </Form.Group>
                            <div className="row">
                                <div className="col-md-6">
                                    <Form.Group className="mb-3">
                                        <Form.Label>Telefone</Form.Label>
                                        <Form.Control name="telefone" value={form.telefone} onChange={handleChange} />
                                    </Form.Group>
                                </div>
                            </div>
                            <Form.Group className="mb-3">
                                <Form.Check type="switch" label="Cliente ativo" name="ativo" checked={form.ativo} onChange={handleChange} />
                            </Form.Group>
                            <button type="submit" className={style.botao_salvar}>Salvar Alterações</button>
                        </Form>
                    </div>
                </div>
            </Topbar>
        </Sidebar>
    );
}
