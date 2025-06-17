export interface Auth {
  // Auth model properties will be added here
}

export interface LoginDto {
    username: string;
    password: string;
}

export interface AuthResponse {
    token: string;
    user: {
        id: string;
        username: string;
        email: string;
    };
}

export interface PasswordResetRequestDto {
    email: string;
}

export interface PasswordResetDto {
    token: string;
    newPassword: string;
    confirmPassword: string;
} 