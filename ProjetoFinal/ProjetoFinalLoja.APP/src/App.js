import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import { Login } from "./paginas/Login/Login";
import { Dashboard } from "./paginas/Dashboard/Dashboard";
import { Produtos } from "./paginas/Produtos/Produtos";
import { NovoProduto } from "./paginas/NovoProduto/NovoProduto";
import { EditarProduto } from "./paginas/EditarProduto/EditarProduto";
import { Clientes } from "./paginas/Clientes/Clientes";
import { NovoCliente } from "./paginas/NovoCliente/NovoCliente";
import { EditarCliente } from "./paginas/EditarCliente/EditarCliente";
import { Pedidos } from "./paginas/Pedidos/Pedidos";
import { AssistenteIA } from "./paginas/AssistenteIA/AssistenteIA";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/login" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/produtos" element={<Produtos />} />
        <Route path="/produto/novo" element={<NovoProduto />} />
        <Route path="/produto/editar" element={<EditarProduto />} />
        <Route path="/clientes" element={<Clientes />} />
        <Route path="/cliente/novo" element={<NovoCliente />} />
        <Route path="/cliente/editar" element={<EditarCliente />} />
        <Route path="/pedidos" element={<Pedidos />} />
        <Route path="/assistente-ia" element={<AssistenteIA />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
