import {HTTPClient} from "./api";

const ProdutoAPI = {
    async listarAsync() {
        try{
            const response = await HTTPClient.get("/Produto/ObterProdutos");
            return response.data;
        } catch (error) {
            console.error("Erro ao listar produtos:", error);
            throw error;
        }
    },

    async obterPorIdAsync(id) {
        try {
            const response = await HTTPClient.get(`/Produto/ObterProdutoPorId/${id}`);
            return response.data;
        } catch (error) {
            console.error("Erro ao obter produto:", error);
            throw error;
        }
    },

    async criarAsync(produto) {
        try {
            const response = await HTTPClient.post("/Produto/CriarProduto", produto);
            return response.data;
        } catch (error) {
            console.error("Erro ao criar produto:", error);
            throw error;
        }
    },

    async atualizarAsync(produto) {
        try {
            const response = await HTTPClient.put("/Produto/AtualizarProduto", produto);
            return response.data;
        } catch (error) {
            console.error("Erro ao atualizar produto:", error);
            throw error;
        }
    },

    async deletarAsync(id) {
        try {
            const response = await HTTPClient.delete(`/Produto/ExcluirProduto/${id}`);
            return response.data;
        } catch (error) {
            console.error("Erro ao deletar produto:", error);
            throw error;
        }
    },

    async ativarAsync(id) {
        try {
            const response = await HTTPClient.put(`/Produto/AtivarProduto/${id}`);
            return response.data;
        } catch (error) {
            console.error("Erro ao ativar produto:", error);
            throw error;
        }
    }   

};

export default ProdutoAPI;
