export interface AuthResponse {

    isSuccess: boolean;

    message: string;

    expiration: string;

    token: string;

    refreshToken: string;

}