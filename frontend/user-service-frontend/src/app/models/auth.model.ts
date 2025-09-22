export interface Auth {
  // Auth model properties will be added here
}

export interface LoginDto {
  userName: string;
  password: string;
}

export enum UserRole {
  User = 1,
  Administrator = 2
}

export interface RegisterDto {
  userName: string;
  email: string;
  password: string; 
  role: UserRole;
}

export interface AuthResponse {
  accessToken: string;
  refreshToken: string;
}

export interface PasswordResetRequestDto {
  email: string;
}

export interface ResetPasswordDto {
  email: string;
  newPassword: string;
  resetToken: string;
} 