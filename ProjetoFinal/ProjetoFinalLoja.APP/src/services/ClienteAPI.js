import {HTTPClient} from "./api";

const ClienteAPI = {
    async listarAsync() {
        try {
            const response = await HTTPClient.get("/Cliente/ObterClientes");
            return response.data;
        } catch (error) {
            console.error("Erro ao listar clientes:", error);
            throw error;
        }
    },

    async obterPorIdAsync(id) {
        try {
            const response = await HTTPClient.get(`/Cliente/ObterClientePorId/${id}`);
            return response.data;
        } catch (error) {
            console.error("Erro ao obter cliente:", error);
            throw error;
        }
    },

    async criarAsync(nome, email, telefone, tipoUsuario, senha){
        try {
            const clienteCriar = {
                Nome: nome,
                Email: email,   
                Telefone: telefone,
                Senha: senha,
                TipoUsuarioId: tipoUsuario,
            };
            const response = await HTTPClient.post("/Cliente/CriarCliente", clienteCriar);
            return response.data;
        } catch (error) {
            console.error("Erro ao criar cliente:", error);
            throw error;
        }
    },

    async atualizarAsync(cliente) {
        try {
            const response = await HTTPClient.put("/Cliente/AtualizarCliente", cliente);
            return response.data;
        } catch (error) {
            console.error("Erro ao atualizar cliente:", error);
            throw error;
        }
    },

    async deletarAsync(id) {
        try {
            const response = await HTTPClient.delete(`/Cliente/ExcluirCliente/${id}`);
            return response.data;
        } catch (error) {
            console.error("Erro ao deletar cliente:", error);
            throw error;
        }
    },

    async ListarTiposUsuariosAsync() {
        try {
            const response = await HTTPClient.get("/TiposUsuario/ListarTiposUsuario");
            return response.data;
        } catch (error) {
            console.error("Erro ao listar tipos de usuários:", error);
            throw error;
        }
    },

    async EnderecoAsync(endereco) {
        try {
            const response = await HTTPClient.post("/Endereco/CriarEndereco", endereco);
            return response.data;
        } catch (error) {
            console.error("Erro ao cadastrar endereço:", error);
            throw error;
        }
    },

    async ativarAsync(id) {
        try {
            const response = await HTTPClient.put(`/Cliente/AtivarCliente/${id}`);
            return response.data;
        } catch (error) {
            console.error("Erro ao ativar cliente:", error);
            throw error;
        }
    },
};

export default ClienteAPI;
