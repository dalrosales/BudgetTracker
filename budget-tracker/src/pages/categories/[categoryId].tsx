// src/pages/categories/[categoryId].tsx

import React, { useState, useEffect } from 'react';
import { useRouter } from 'next/router';
import Layout from '../../components/Layout';
import { fetchCategoryById } from '../../lib/api';

interface Category {
  id: number;
  name: string;
}

const CategoryPage: React.FC = () => {
  const router = useRouter();
  const { categoryId } = router.query;
  const [category, setCategory] = useState<Category | null>(null);

  useEffect(() => {
    const fetchCategory = async () => {
      try {
        if (typeof categoryId === 'string') {
          const categoryData = await fetchCategoryById(parseInt(categoryId, 10));
          setCategory(categoryData);
        }
      } catch (error) {
        console.error('Error fetching category:', error);
      }
    };

    fetchCategory();
  }, [categoryId]);

  if (!category) {
    return <div>Loading...</div>;
  }

  return (
    <Layout>
      <div>
        <h2>Category Details</h2>
        <p>ID: {category.id}</p>
        <p>Name: {category.name}</p>
      </div>
    </Layout>
  );
};

export default CategoryPage;
