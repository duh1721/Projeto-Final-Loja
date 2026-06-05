import { Sidebar } from "../../componentes/Sidebar/Sidebar";
import { Topbar } from "../../componentes/Topbar/Topbar";
import style from "./Dashboard.module.css";
import { useEffect, useState } from "react";
import Table from "react-bootstrap/Table";
import { MdInventory2, MdPeople, MdShoppingCart, MdAttachMoney } from "react-icons/md";
import ProdutoAPI from "../../services/ProdutoAPI";
import ClienteAPI from "../../services/ClienteAPI";
import PedidoAPI from "../../services/PedidoAPI";
import TiposProdutosAPI from "../../services/TiposProdutos";

export function Dashboard() {
    const [totalProdutos, setTotalProdutos] = useState(0);
    const [totalClientes, setTotalClientes] = useState(0);
    const [totalPedidos, setTotalPedidos] = useState(0);
    const [receitaTotal, setReceitaTotal] = useState(0);
    const [ultimosPedidos, setUltimosPedidos] = useState([]);
    const [MostrartiposProdutos, setTiposProdutos] = useState(0);

    useEffect(() => {
        async function carregarDados() {
            try {
                const [produtos, clientes, pedidos, tiposProdutos] = await Promise.all([
                    ProdutoAPI.listarAsync(),
                    ClienteAPI.listarAsync(),
                    PedidoAPI.listarAsync(),
                    TiposProdutosAPI.listarAsync()
                ]);
                setTotalProdutos(produtos.length);
                setTotalClientes(clientes.length);
                setTotalPedidos(pedidos.length);
                setTiposProdutos(tiposProdutos.length);
                const receita = pedidos.reduce((s, p) => s + (p.valorTotal || 0), 0);
                setReceitaTotal(receita);
                setUltimosPedidos(pedidos.slice(-5).reverse());
            } catch (error) {
                console.error("Erro ao carregar dashboard:", error);
            }
        }
        
        carregarDados();
    }, []);

    return (
        <Sidebar>
            <Topbar titulo="Dashboard">
                <div className={style.pagina_conteudo}>
                    <div className={style.cabecalho}>
                        <h3>Dashboard</h3>
                        <p>Visão geral do sistema</p>
                    </div>

                    <div className={style.cards_grid}>
                        
                        <div className={`${style.card_stat} ${style.card_stat_azul}`}>
                            <div className={style.card_stat_label}>Produtos</div>
                            <div className={style.card_stat_valor}>{totalProdutos}</div>
                            <MdInventory2 className={style.card_stat_icone} />
                        </div>

                        <div className={`${style.card_stat} ${style.card_stat_verde}`}>
                            <div className={style.card_stat_label}>Clientes</div>
                            <div className={style.card_stat_valor}>{totalClientes}</div>
                            <MdPeople className={style.card_stat_icone} />
                        </div>

                        <div className={`${style.card_stat} ${style.card_stat_laranja}`}>
                            <div className={style.card_stat_label}>Pedidos</div>
                            <div className={style.card_stat_valor}>{totalPedidos}</div>
                            <MdShoppingCart className={style.card_stat_icone} />
                        </div>

                        <div className={`${style.card_stat} ${style.card_stat_roxo}`}>
                            <div className={style.card_stat_label}>Receita Total</div>
                            <div className={style.card_stat_valor}>R$ {receitaTotal.toFixed(2)}</div>
                            <MdAttachMoney className={style.card_stat_icone} />
                        </div>

                        <div className={`${style.card_stat} ${style.card_stat_rosa}`}>
                            <div className={style.card_stat_label}>Tipos de Produtos</div>
                            <div className={style.card_stat_valor}>{MostrartiposProdutos}</div>
                            <MdInventory2 className={style.card_stat_icone} />
                        </div>

                    </div>

                    <div className={style.tabela_wrap}>
                        <div className={style.secao_titulo}>Últimos Pedidos</div>
                        <Table responsive hover>
                            <thead>
                                <tr>
                                    <th>#ID</th>
                                    <th>Cliente ID</th>
                                    <th>Data</th>
                                    <th>Total</th>
                                </tr>
                            </thead>
                            <tbody>
                                {ultimosPedidos.length === 0 ? (
                                    <tr><td colSpan={4} className="text-center text-muted py-4">Nenhum pedido ainda</td></tr>
                                ) : ultimosPedidos.map((p) => (
                                    <tr key={p.id}>
                                        <td>#{p.id}</td>
                                        <td>{p.clienteId}</td>
                                        <td>{new Date(p.dataPedido).toLocaleDateString("pt-BR")}</td>
                                        <td><strong>R$ {(p.valorTotal || 0).toFixed(2)}</strong></td>
                                    </tr>
                                ))}
                            </tbody>
                        </Table>
                    </div>
                </div>
            </Topbar>
        </Sidebar>
    );
}
