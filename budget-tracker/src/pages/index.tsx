// /src/pages/index.tsx

import React from 'react';
import Layout from '../components/Layout';
import CategoryList from '../components/CategoryList';

const Home: React.FC = () => {
  return (
    <Layout>
      <div>
        <h1>Welcome to Budget Tracker App!</h1>
        {/* Render the CategoryList component */}
        <CategoryList />
      </div>
    </Layout>
  );
};

export default Home;
