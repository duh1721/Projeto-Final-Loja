import { Sidebar } from "../../componentes/Sidebar/Sidebar";
import { Topbar } from "../../componentes/Topbar/Topbar";
import { Link } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import { MdEdit, MdDelete, MdCheckCircle } from "react-icons/md";
import { useEffect, useState } from "react";
import ProdutoAPI from "../../services/ProdutoAPI";
import style from "./Produto.module.css";

export function Produtos() {
    const [produtos, setProdutos] = useState([]);
    const [busca, setBusca] = useState("");
    const [mostrarModal, setMostrarModal] = useState(false);
    const [produtoSelecionado, setProdutoSelecionado] = useState(null);

    useEffect(() => { carregarProdutos(); }, []);

    async function carregarProdutos() {
        try {
            const lista = await ProdutoAPI.listarAsync();
            setProdutos(Array.isArray(lista) ? lista : []);
        } catch (error) {
            console.error("Erro ao carregar produtos:", error);
        }
    }

    const handleClickDeletar = (produto) => {
        setProdutoSelecionado(produto);
        setMostrarModal(true);
    };

    const handleDeletar = async () => {
        try {
            await ProdutoAPI.deletarAsync(produtoSelecionado.id);

            setProdutos(listaAtual => listaAtual.map(p => p.id === produtoSelecionado.id ? { ...p, ativo: false } : p));

        } catch (error) {
            console.error("Erro ao deletar:", error);
        } finally {
            setMostrarModal(false);
            setProdutoSelecionado(null);
        }
    };

    const handleAtivar = async (id) => {
        try {
            await ProdutoAPI.ativarAsync(id);
            carregarProdutos();
        } catch (error) { console.error(error); }
    };

    const produtosFiltrados = produtos.filter(p =>
        (p.nome || "").toLowerCase().includes(busca.toLowerCase())
    );

    return (
        <Sidebar>
            <Topbar titulo="Produtos">
                <div className={style.pagina_conteudo}>
                    <div className={style.pagina_cabecalho}>
                        <h3>Produtos</h3>
                        <div className={style.cabecalho_acoes}>
                            <input
                                type="text"
                                placeholder="Buscar produto..."
                                value={busca}
                                onChange={(e) => setBusca(e.target.value)}
                                className={style.input_busca}
                            />
                            <Link to="/produto/novo" className={style.botao_novo}>+ Novo</Link>
                        </div>
                    </div>

                    <div className={style.tabela}>
                        <Table responsive hover>
                            <thead className={style.tabela_cabecalho}>
                                <tr>
                                    <th>#ID</th>
                                    <th>Nome</th>
                                    <th>Descrição</th>
                                    <th>Preço</th>
                                    <th>Estoque</th>
                                    <th>Status</th>
                                    <th>Ações</th>
                                </tr>
                            </thead>
                            <tbody>
                                {produtosFiltrados.length === 0 ? (
                                    <tr><td colSpan={7} className="text-center text-muted py-4">Nenhum produto encontrado</td></tr>
                                ) : produtosFiltrados.map((produto) => (
                                    <tr key={produto.id}>
                                        <td className="text-muted">#{produto.id}</td>
                                        <td><strong>{produto.nome}</strong></td>
                                        <td className="text-muted">{produto.descricao || "—"}</td>
                                        <td><strong>R$ {(produto.preco || 0).toFixed(2)}</strong></td>
                                        <td>{produto.quantidade ?? "—"}</td>
                                        <td>
                                            <span className={produto.ativo ? style.badge_ativo : style.badge_inativo}>
                                                {produto.ativo ? "Ativo" : "Inativo"}
                                            </span>
                                        </td>
                                        <td>
                                            <Link to="/produto/editar" state={produto.id} className={style.botao_editar}>
                                                <MdEdit />
                                            </Link>

                                            {!produto.ativo && (
                                                <button
                                                    onClick={() => handleAtivar(produto.id)}
                                                    className={style.botao_editar}
                                                    title="Ativar produto"
                                                >
                                                    <MdCheckCircle />
                                                </button>
                                            )}

                                            {produto.ativo && (
                                                <button
                                                    onClick={() => handleClickDeletar(produto)}
                                                    className={style.botao_deletar}
                                                    title="Desativar produto"
                                                >
                                                    <MdDelete />
                                                </button>
                                            )}
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </Table>
                    </div>

                    <Modal show={mostrarModal} onHide={() => setMostrarModal(false)}>
                        <Modal.Header closeButton>
                            <Modal.Title>Confirmar exclusão</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            Tem certeza que deseja excluir o produto <strong>{produtoSelecionado?.nome}</strong>?
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={() => setMostrarModal(false)}>Cancelar</Button>
                            <Button variant="danger" onClick={handleDeletar}>Excluir</Button>
                        </Modal.Footer>
                    </Modal>
                </div>
            </Topbar>
        </Sidebar>
    );
}
