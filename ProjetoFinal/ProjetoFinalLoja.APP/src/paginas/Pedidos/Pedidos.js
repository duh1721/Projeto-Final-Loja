import { Sidebar } from "../../componentes/Sidebar/Sidebar";
import { Topbar } from "../../componentes/Topbar/Topbar";
import Table from "react-bootstrap/Table";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import { MdDelete, MdCheckCircle } from "react-icons/md";
import { useEffect, useState } from "react";
import PedidoAPI from "../../services/PedidoAPI";
import style from "./Pedido.module.css";

export function Pedidos() {
    const [pedidos, setPedidos] = useState([]);
    const [busca, setBusca] = useState("");
    const [mostrarModal, setMostrarModal] = useState(false);
    const [pedidoSelecionado, setPedidoSelecionado] = useState(null);

    useEffect(() => { carregarPedidos(); }, []);

    async function carregarPedidos() {
        try {
            const lista = await PedidoAPI.listarAsync();
            setPedidos(Array.isArray(lista) ? lista : []);
        } catch (error) { console.error("Erro ao carregar pedidos:", error); }
    }

    const handleClickDeletar = (pedido) => { setPedidoSelecionado(pedido); setMostrarModal(true); };

    const handleDeletar = async () => {
        try {
            await PedidoAPI.deletarAsync(pedidoSelecionado.id);
            setPedidos(listaAtual => listaAtual.map(p => p.id === pedidoSelecionado.id ? { ...p, ativo: false } : p));
        } catch (error) { console.error(error); }
        finally { setMostrarModal(false); setPedidoSelecionado(null); }
    };

    const handleAtivar = async (id) => {
        try {
            await PedidoAPI.ativarAsync(id);
            carregarPedidos();
        } catch (error) { console.error(error); }
    };

    const pedidosFiltrados = pedidos.filter(p =>
        String(p.id).includes(busca) || String(p.clienteId).includes(busca)
    );

    return (
        <Sidebar>
            <Topbar titulo="Pedidos">
                <div className={style.pagina_conteudo}>
                    <div className={style.pagina_cabecalho}>
                        <h3>Pedidos</h3>
                        <input type="text" placeholder="Buscar por ID ou cliente..." value={busca}
                            onChange={(e) => setBusca(e.target.value)} className={style.input_busca} />
                    </div>
                    <div className={style.tabela}>
                        <Table responsive hover>
                            <thead className={style.tabela_cabecalho}>
                                <tr><th>#ID</th><th>Cliente ID</th><th>Endereço ID</th><th>Data</th><th>Total</th><th>Status</th><th>Ações</th></tr>
                            </thead>
                            <tbody>
                                {pedidosFiltrados.length === 0 ? (
                                    <tr><td colSpan={7} className="text-center text-muted py-4">Nenhum pedido encontrado</td></tr>
                                ) : pedidosFiltrados.map((p) => (
                                    <tr key={p.id}>
                                        <td className="text-muted">#{p.id}</td>
                                        <td>{p.clienteId}</td>
                                        <td>{p.enderecoId}</td>
                                        <td className="text-muted">{new Date(p.dataPedido).toLocaleDateString("pt-BR")}</td>
                                        <td><strong>R$ {(p.valorTotal || 0).toFixed(2)}</strong></td>
                                        <td><span className={p.ativo ? style.badge_ativo : style.badge_inativo}>{p.ativo ? "Ativo" : "Cancelado"}</span></td>
                                        <td>
                                            {!p.ativo && (
                                                <button
                                                    onClick={() => handleAtivar(p.id)}
                                                    className={style.botao_editar}
                                                    title="Ativar produto"
                                                >
                                                    <MdCheckCircle />
                                                </button>
                                            )}

                                            {p.ativo && (
                                                <button
                                                    onClick={() => handleClickDeletar(p)}
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
                        <Modal.Header closeButton><Modal.Title>Cancelar pedido</Modal.Title></Modal.Header>
                        <Modal.Body>Tem certeza que deseja cancelar o pedido <strong>#{pedidoSelecionado?.id}</strong>?</Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={() => setMostrarModal(false)}>Voltar</Button>
                            <Button variant="danger" onClick={handleDeletar}>Cancelar Pedido</Button>
                        </Modal.Footer>
                    </Modal>
                </div>
            </Topbar>
        </Sidebar>
    );
}
