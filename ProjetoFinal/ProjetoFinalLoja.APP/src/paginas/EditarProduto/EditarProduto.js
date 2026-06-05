import { Sidebar } from "../../componentes/Sidebar/Sidebar";
import { Topbar } from "../../componentes/Topbar/Topbar";
import { Link, useNavigate, useLocation } from "react-router-dom";
import Form from "react-bootstrap/Form";
import { useState, useEffect } from "react";
import ProdutoAPI from "../../services/ProdutoAPI";
import style from "./EditarProduto.module.css";
import { MdArrowBack } from "react-icons/md";
import TiposProdutosAPI from "../../services/TiposProdutos";

export function EditarProduto() {
    const navigate = useNavigate();
    const location = useLocation();
    const id = location.state;
    const [erro, setErro] = useState("");
    const [form, setForm] = useState({
        nome: "", descricao: "", preco: "", quantidade: "", tipoProdutoId: "1", ativo: true
    });
    const [tiposProduto, setTiposProduto] = useState([]);

    useEffect(() => {
        async function carregar() {
            try {
                const produto = await ProdutoAPI.obterPorIdAsync(id);
                setForm({
                    nome: produto.nome || "",
                    descricao: produto.descricao || "",
                    preco: produto.preco || "",
                    quantidade: produto.quantidade || "",
                    tipoProdutoId: produto.tipoProdutoId || 1,
                    ativo: produto.ativo ?? true
                });

                const tipos = await TiposProdutosAPI.listarAsync();
                setTiposProduto(Array.isArray(tipos) ? tipos : []);
            } catch (error) {
                console.error("Erro ao carregar produto:", error);
            }
        }
        if (id) carregar();
    }, [id]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setForm(prev => ({ ...prev, [name]: type === "checkbox" ? checked : value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setErro("");

        try {
            await ProdutoAPI.atualizarAsync({
                id,
                nome: form.nome,
                descricao: form.descricao,
                preco: parseFloat(form.preco),
                quantidade: parseInt(form.quantidade) || 0,
                tipoProdutoId: parseInt(form.tipoProdutoId),
                ativo: form.ativo
            });
            navigate("/produtos");
        } catch (error) {
            setErro("Erro ao atualizar produto.");
            console.error(error);
        }
    };

    return (
        <Sidebar>
            <Topbar titulo="Editar Produto">
                <div className={style.pagina_conteudo}>
                    <div className={style.pagina_cabecalho}>
                        <Link to="/produtos" className={style.botao_voltar}><MdArrowBack /> Voltar</Link>
                        <h3>Editar Produto</h3>
                    </div>
                    <div className={style.card_form}>
                        {erro && <div className={style.erro}>{erro}</div>}
                        <Form onSubmit={handleSubmit}>
                            <Form.Group className="mb-3">
                                <Form.Label>Nome *</Form.Label>
                                <Form.Control name="nome" value={form.nome} onChange={handleChange} required />
                            </Form.Group>
                            <Form.Group className="mb-3">
                                <Form.Label>Descrição</Form.Label>
                                <Form.Control as="textarea" rows={2} name="descricao" value={form.descricao} onChange={handleChange} />
                            </Form.Group>
                            <div className="row">
                                <div className="col-md-6">
                                    <Form.Group className="mb-3">
                                        <Form.Label>Preço (R$) *</Form.Label>
                                        <Form.Control type="number" step="0.01" min="0" name="preco" value={form.preco} onChange={handleChange} required />
                                    </Form.Group>
                                </div>
                                <div className="col-md-6">
                                    <Form.Group className="mb-3">
                                        <Form.Label>Quantidade</Form.Label>
                                        <Form.Control type="number" min="0" name="quantidade" value={form.quantidade} onChange={handleChange} />
                                    </Form.Group>
                                </div>
                            </div>

                            <Form.Group className="mb-3">
                                <Form.Label>Tipo de Produto</Form.Label>
                                <Form.Control as="select" name="tipoProdutoId" value={form.tipoProdutoId} onChange={handleChange}>
                                    <option value="">Selecione um tipo</option>
                                    {tiposProduto.map(tipo => (
                                        <option key={tipo.id} value={tipo.id}>{tipo.nome}</option>
                                    ))}
                                </Form.Control>
                            </Form.Group>

                            <Form.Group className="mb-3">
                                <Form.Check type="switch" label="Produto ativo" name="ativo" checked={form.ativo} onChange={handleChange} />
                            </Form.Group>

                            <button type="submit" className={style.botao_salvar}>Salvar Alterações</button>
                        </Form>
                    </div>
                </div>
            </Topbar>
        </Sidebar>
    );
}
