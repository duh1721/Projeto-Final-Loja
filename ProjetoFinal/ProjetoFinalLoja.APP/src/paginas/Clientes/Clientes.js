import { Sidebar } from "../../componentes/Sidebar/Sidebar";
import { Topbar } from "../../componentes/Topbar/Topbar";
import { Link } from "react-router-dom";
import Table from "react-bootstrap/Table";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import { MdEdit, MdDelete, MdCheckCircle } from "react-icons/md";
import { useEffect, useState } from "react";
import ClienteAPI from "../../services/ClienteAPI";
import style from "./Cliente.module.css";

export function Clientes() {
  const [clientes, setClientes] = useState([]);
  const [busca, setBusca] = useState("");
  const [mostrarModal, setMostrarModal] = useState(false);
  const [clienteSelecionado, setClienteSelecionado] = useState(null);
  const [tiposUsuarios, setTiposUsuarios] = useState([]);

  useEffect(() => {
    carregarClientes();
    TiposUsuarios();
  }, []);

  async function carregarClientes() {
    try {
      const lista = await ClienteAPI.listarAsync();
      setClientes(Array.isArray(lista) ? lista : []);
    } catch (error) {
      console.error("Erro ao carregar clientes:", error);
    }
  }

  async function TiposUsuarios() {
    try {
      const lista = await ClienteAPI.ListarTiposUsuariosAsync();
      setTiposUsuarios(Array.isArray(lista) ? lista : []);
    } catch (error) {
      console.error("ERRO TIPOS:", error);
    }
  }

  const handleClickDeletar = (cliente) => {
    setClienteSelecionado(cliente);
    setMostrarModal(true);
  };

  const handleDeletar = async () => {
    if (clienteSelecionado == null) return;

    const id = clienteSelecionado.id || clienteSelecionado.ID;

    try {
      await ClienteAPI.deletarAsync(id);

      setMostrarModal(false);
      setClienteSelecionado(null);

        setClientes(listaAtual => listaAtual.map(p => p.id === clienteSelecionado.id ? { ...p, ativo: false } : p));

    } catch (error) {
      console.error("Erro ao deletar:", error);
    }
  };

  const handleAtivar = async (id) => {
    try {
      await ClienteAPI.ativarAsync(id);
      carregarClientes();
    } catch (error) {
      console.error(error);
    }
  };

  const clientesFiltrados = clientes.filter(
    (c) =>
      (c.nome || "").toLowerCase().includes(busca.toLowerCase()) ||
      (c.email || "").toLowerCase().includes(busca.toLowerCase()),
  );

  function renderizarlinha() {
    if (clientesFiltrados.length === 0) {
      return (
        <tr>
          <td colSpan={8} className="text-center text-muted py-4">
            Nenhum cliente encontrado
          </td>
        </tr>
      );
    } else {
      return clientesFiltrados.map((c) => {
        return (
          <tr key={c.id}>
            <td className="text-muted">#{c.id}</td>
            <td>
              <strong>{c.nome}</strong>
            </td>
            <td className="text-muted">{c.email || "—"}</td>
            <td>{c.telefone || "—"}</td>
            <td>
              <span
                className={c.ativo ? style.badge_ativo : style.badge_inativo}
              >
                {c.ativo ? "Ativo" : "Inativo"}
              </span>
            </td>
            <td>
              <Link
                to="/cliente/editar"
                state={c.id}
                className={style.botao_editar}
              >
                <MdEdit />
              </Link>
              {!c.ativo && (
                <button
                  onClick={() => handleAtivar(c.id)}
                  className={style.botao_editar}
                  title="Ativar cliente"
                >
                  <MdCheckCircle />
                </button>
              )}

              {c.ativo && (
                <button
                  onClick={() => handleClickDeletar(c)}
                  className={style.botao_deletar}
                  title="Desativar produto"
                >
                  <MdDelete />
                </button>
              )}
            </td>

            <td>
              {tiposUsuarios.find((t) => t.id === c.tipoUsuarioId)?.nome || "—"}
            </td>
          </tr>
        );
      });
    }
  }

  return (
    <Sidebar>
      <Topbar titulo="Clientes">
        <div className={style.pagina_conteudo}>
          <div className={style.pagina_cabecalho}>
            <h3>Clientes</h3>
            <div className={style.cabecalho_acoes}>
              <input
                type="text"
                placeholder="Buscar cliente..."
                value={busca}
                onChange={(e) => setBusca(e.target.value)}
                className={style.input_busca}
              />
              <Link to="/cliente/novo" className={style.botao_novo}>
                + Novo
              </Link>
            </div>
          </div>
          <div className={style.tabela}>
            <Table responsive hover>
              <thead className={style.tabela_cabecalho}>
                <tr>
                  <th>#ID</th>
                  <th>Nome</th>
                  <th>Email</th>
                  <th>Telefone</th>
                  <th>Status</th>
                  <th>Ações</th>
                  <th>Tipos de Usuários</th>
                </tr>
              </thead>
              <tbody>{renderizarlinha()}</tbody>
            </Table>
          </div>
          <Modal show={mostrarModal} onHide={() => setMostrarModal(false)}>
            <Modal.Header closeButton>
              <Modal.Title>Confirmar exclusão</Modal.Title>
            </Modal.Header>
            <Modal.Body>
              Tem certeza que deseja excluir o cliente{" "}
              <strong>{clienteSelecionado?.nome}</strong>?
            </Modal.Body>
            <Modal.Footer>
              <Button
                variant="secondary"
                onClick={() => setMostrarModal(false)}
              >
                Cancelar
              </Button>
              <Button variant="danger" onClick={handleDeletar}>
                Excluir
              </Button>
            </Modal.Footer>
          </Modal>
        </div>
      </Topbar>
    </Sidebar>
  );
}
