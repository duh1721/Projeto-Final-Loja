import {HTTPClient} from "./api";

const TiposProdutosAPI = {
    async listarAsync() {
        try {
            const response = await HTTPClient.get("TipoProduto/ObterTiposProduto");
            return response.data;
        } catch (error) {
            console.error("Erro ao listar tipos de produtos:", error);
            throw error;
        }
    },

    async obterPorIdAsync(id) {
        try {
            const response = await HTTPClient.get(`TipoProduto/ObterTipoProdutoPorId/${id}`);
            return response.data;
        } catch (error) {
            console.error("Erro ao obter tipo de produto:", error);
            throw error;
        }
    },

    async criarAsync(tipoProduto) {
        try {
            const response = await HTTPClient.post("TipoProduto/CriarTipoProduto", tipoProduto);
            return response.data;
        } catch (error) {
            console.error("Erro ao criar tipo de produto:", error);
            throw error;
        }
    },

    async atualizarAsync(tipoProduto) {
        try {
            const response = await HTTPClient.put("TipoProduto/AtualizarTipoProduto", tipoProduto);
            return response.data;
        } catch (error) {
            console.error("Erro ao atualizar tipo de produto:", error);
            throw error;
        }
    },

    async deletarAsync(id) {
        try {
            const response = await HTTPClient.delete(`TipoProduto/ExcluirTipoProduto/${id}`);
            return response.data;
        } catch (error) {
            console.error("Erro ao deletar tipo de produto:", error);
            throw error;
        }
    }
};

export default TiposProdutosAPI;