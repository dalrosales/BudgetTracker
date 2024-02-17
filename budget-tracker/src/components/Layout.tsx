import React, { ReactNode } from 'react';
import Head from 'next/head';
import { Inter } from 'next/font/google';
import '../styles/globals.css';

const inter = Inter({ subsets: ['latin'] });

interface LayoutProps {
  children: ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <>
      <Head>
        <title>Budget Tracker App</title>
        <meta name="description" content="Your budget tracking application description goes here" />
        {/* Add any other meta tags or links to external stylesheets or scripts */}
      </Head>
      
      <body className={inter.className}>
        {/* Common layout structure */}
        {children}
      </body>
    </>
  );
};

export default Layout;
