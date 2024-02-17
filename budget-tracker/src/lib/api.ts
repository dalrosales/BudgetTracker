// src/lib/api.ts

interface Category {
    id: number;
    name: string;
  }
  
  export const fetchCategories = async (): Promise<Category[]> => {
    try {
      const response = await fetch('https://budgettrackerapi.azurewebsites.net/api/categories');
      if (!response.ok) {
        throw new Error('Failed to fetch categories');
      }
      const data = await response.json();
      return data;
    } catch (error) {
      throw error;
    }
  };
  