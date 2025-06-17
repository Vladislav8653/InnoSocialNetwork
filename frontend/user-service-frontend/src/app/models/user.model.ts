export interface User {
  id: string;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  dateOfBirth: Date;
  profilePictureUrl: string;
  bio: string;
  isActive: boolean;
  createdAt: Date;
  updatedAt: Date;
}

export interface UserRegistrationDto {
  username: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  dateOfBirth: Date;
}

export interface UserUpdateDto {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  dateOfBirth: Date;
  bio: string;
} 