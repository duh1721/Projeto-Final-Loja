import { Sidebar } from "../../componentes/Sidebar/Sidebar";
import { Topbar } from "../../componentes/Topbar/Topbar";
import { Link, useNavigate } from "react-router-dom";
import Form from "react-bootstrap/Form";
import { useState, useEffect } from "react";
import ProdutoAPI from "../../services/ProdutoAPI";
import style from "./NovoProduto.module.css";
import { MdArrowBack } from "react-icons/md";
import TiposProdutosAPI from "../../services/TiposProdutos";

export function NovoProduto() {
    const navigate = useNavigate();
    const [erro, setErro] = useState("");
    const [form, setForm] = useState({
        nome: "", descricao: "", preco: "", quantidade: "", tipoProdutoId: "1", ativo: true
    });
    const [tipoProduto, setTipoProduto] = useState([]);

    useEffect(() => {
        async function carregarTipos() {
            try {
                const tipos = await TiposProdutosAPI.listarAsync();
                setTipoProduto(Array.isArray(tipos) ? tipos : []);
            } catch (error) {
                console.error("Erro ao carregar tipos de produtos:", error);
            }
        }
        carregarTipos();
    }, []);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setForm(prev => ({ ...prev, [name]: type === "checkbox" ? checked : value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setErro("");
        if (!form.nome || !form.preco) { setErro("Nome e preço são obrigatórios."); return; }
        try {
            await ProdutoAPI.criarAsync({
                nome: form.nome,
                descricao: form.descricao,
                preco: parseFloat(form.preco),
                quantidade: parseInt(form.quantidade) || 0,
                tipoProdutoId: parseInt(form.tipoProdutoId),
                ativo: form.ativo
            });
            navigate("/produtos");
        } catch (error) {
            setErro("Erro ao salvar produto. Tente novamente.");
            console.error(error);
        }
    };

    return (
        <Sidebar>
            <Topbar titulo="Novo Produto">
                <div className={style.pagina_conteudo}>
                    <div className={style.pagina_cabecalho}>
                        <Link to="/produtos" className={style.botao_voltar}><MdArrowBack /> Voltar</Link>
                        <h3>Novo Produto</h3>
                    </div>

                    <div className={style.card_form}>
                        {erro && <div className={style.erro}>{erro}</div>}
                        <Form onSubmit={handleSubmit}>
                            <Form.Group className="mb-3">
                                <Form.Label>Nome *</Form.Label>
                                <Form.Control name="nome" value={form.nome} onChange={handleChange} placeholder="Nome do produto" required />
                            </Form.Group>
                            <Form.Group className="mb-3">
                                <Form.Label>Descrição</Form.Label>
                                <Form.Control as="textarea" rows={2} name="descricao" value={form.descricao} onChange={handleChange} placeholder="Descrição do produto" />
                            </Form.Group>
                            <div className="row">
                                <div className="col-md-6">
                                    <Form.Group className="mb-3">
                                        <Form.Label>Preço (R$) *</Form.Label>
                                        <Form.Control type="number" step="0.01" min="0" name="preco" value={form.preco} onChange={handleChange} placeholder="0.00" required />
                                    </Form.Group>
                                </div>
                                <div className="col-md-6">
                                    <Form.Group className="mb-3">
                                        <Form.Label>Quantidade</Form.Label>
                                        <Form.Control type="number" min="0" name="quantidade" value={form.quantidade} onChange={handleChange} placeholder="0" />
                                    </Form.Group>
                                </div>
                            </div>
                            <Form.Group className="mb-3">
                                <Form.Label>Tipo de Produto </Form.Label>
                                <Form.Control as="select" name="tipoProdutoId" value={form.tipoProdutoId} onChange={handleChange}>
                                    <option value="">Selecione um tipo</option>
                                    {tipoProduto.map(tipo => (
                                        <option key={tipo.id} value={tipo.id}>{tipo.nome}</option>
                                    ))}
                                </Form.Control>
                            </Form.Group>
                            <Form.Group className="mb-3">
                                <Form.Check type="switch" label="Produto ativo" name="ativo" checked={form.ativo} onChange={handleChange} />
                            </Form.Group>
                            <button type="submit" className={style.botao_salvar}>Salvar Produto</button>
                        </Form>
                    </div>
                </div>
            </Topbar>
        </Sidebar>
    );
}
