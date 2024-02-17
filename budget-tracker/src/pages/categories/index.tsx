// src/pages/categories/index.tsx

import React from 'react';
import Layout from '../../components/Layout';
import CategoryList from '../../components/CategoryList';

const CategoriesPage: React.FC = () => {
  return (
    <Layout>
      <CategoryList />
    </Layout>
  );
};

export default CategoriesPage;
