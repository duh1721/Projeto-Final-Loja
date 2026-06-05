import { HTTPClient } from "./api";

const LoginAPI = {
  async loginAsync(email, senha) {
    try {
        const response = await HTTPClient.post("/Login/Login", { email, senha });
        return response.data;
    } catch (error) {
        console.error("Erro ao fazer login:", error);
        throw error;
    }   
    }
};

export default LoginAPI;