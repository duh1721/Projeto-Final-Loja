import {HTTPClient} from "./api";

const PedidoAPI = {
    async listarAsync() {
        try {
            const response = await HTTPClient.get("/Pedido/ObterPedidos");
            return response.data;
        } catch (error) {
            console.error("Erro ao listar pedidos:", error);
            throw error;
        }
    },

    async obterPorIdAsync(id) {
        try {
            const response = await HTTPClient.get(`/Pedido/ObterPedidoPorId/${id}`);
            return response.data;
        } catch (error) {
            console.error("Erro ao obter pedido:", error);
            throw error;
        }
    },

    async criarAsync(pedido) {
        try {
            const response = await HTTPClient.post("/Pedido/CriarPedido", pedido);
            return response.data;
        } catch (error) {
            console.error("Erro ao criar pedido:", error);
            throw error;
        }
    },

    async deletarAsync(id) {
        try {
            const response = await HTTPClient.delete(`/Pedido/ExcluirPedido/${id}`);
            return response.data;
        } catch (error) {
            console.error("Erro ao deletar pedido:", error);
            throw error;
        }
    },

    async ativarAsync(id) {
        try {
            const response = await HTTPClient.put(`/Pedido/AtivarPedido/${id}`);
            return response.data;
        } catch (error) {
            console.error("Erro ao ativar pedido:", error);
            throw error;
        }
    }
};

export default PedidoAPI;
