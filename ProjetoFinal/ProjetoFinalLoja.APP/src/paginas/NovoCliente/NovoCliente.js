import { Sidebar } from "../../componentes/Sidebar/Sidebar";
import { Topbar } from "../../componentes/Topbar/Topbar";
import { Link, useNavigate } from "react-router-dom";
import Form from "react-bootstrap/Form";
import { useState } from "react";
import ClienteAPI from "../../services/ClienteAPI";
import style from "./NovoCliente.module.css";
import { MdArrowBack } from "react-icons/md";

export function NovoCliente() {
    const navigate = useNavigate();
    const [erro, setErro] = useState("");
    const [form, setForm] = useState({ nome: "", email: "", telefone: "", tipoUsuario: "", senha: "", ativo: true });

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setForm(prev => ({ ...prev, [name]: type === "checkbox" ? checked : value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setErro("");
        if (!form.nome) { setErro("Nome é obrigatório."); return; }
        try {
            await ClienteAPI.criarAsync(form.nome, form.email, form.telefone, form.tipoUsuario, form.senha);
            navigate("/clientes");
        } catch (error) {
            setErro("Erro ao salvar cliente.");
            console.error(error);
        }
    };

    return (
        <Sidebar>
            <Topbar titulo="Novo Cliente">
                <div className={style.pagina_conteudo}>
                    <div className={style.pagina_cabecalho}>
                        <Link to="/clientes" className={style.botao_voltar}><MdArrowBack /> Voltar</Link>
                        <h3>Novo Cliente</h3>
                    </div>
                    <div className={style.card_form}>
                        {erro && <div className={style.erro}>{erro}</div>}
                        <Form onSubmit={handleSubmit}>

                            <Form.Group className="mb-3">
                                <Form.Label>Nome *</Form.Label>
                                <Form.Control name="nome" value={form.nome} onChange={handleChange} placeholder="Nome completo" required />
                            </Form.Group>

                            <Form.Group className="mb-3">
                                <Form.Label>Email</Form.Label>
                                <Form.Control type="email" name="email" value={form.email} onChange={handleChange} placeholder="email@exemplo.com" />
                            </Form.Group>

                            <div className="row">
                                <div className="col-md-6">
                                    <Form.Group className="mb-3">
                                        <Form.Label>Telefone</Form.Label>
                                        <Form.Control name="telefone" value={form.telefone} onChange={handleChange} placeholder="(00) 00000-0000" />
                                    </Form.Group>
                                </div>

                                <div className="col-md-6">
                                    <Form.Group className="mb-3">
                                        <Form.Label>Senha</Form.Label>
                                        <Form.Control type="password" name="senha" value={form.senha} onChange={handleChange} placeholder="Digite a senha" />
                                    </Form.Group>
                                </div>

                                <div className="col-md-6">
                                    <Form.Group className="mb-3">
                                        <Form.Label>Tipo de Usuario</Form.Label>
                                        <Form.Select name="tipoUsuario" value={form.tipoUsuario} onChange={handleChange}>
                                            <option value="">Selecione o tipo</option>
                                            <option value="1">Administrador</option>
                                            <option value="2">Cliente</option>
                                        </Form.Select>
                                    </Form.Group>
                                </div>

                            </div>

                            <Form.Group className="mb-3">
                                <Form.Check type="switch" label="Cliente ativo" name="ativo" checked={form.ativo} onChange={handleChange} />
                            </Form.Group>

                            <button type="submit" className={style.botao_salvar}>Salvar Cliente</button>

                        </Form>
                    </div>
                </div>
            </Topbar>
        </Sidebar>
    );
}
