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