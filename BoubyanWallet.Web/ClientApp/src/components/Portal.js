import React from 'react';

export function Portal() {
  return <>
    <h1>Portal</h1>

    <h2>Transfer</h2>
    <form>
      <label htmlFor="amount" className="block text-sm font-medium text-gray-700 sm:mt-px sm:pt-2">
        Amount 
      </label>
      <input
        type="text"
        name="amount"
        id="amount"
        className="flex-1 block w-full focus:ring-indigo-500 focus:border-indigo-500 min-w-0 rounded-none rounded-r-md sm:text-sm border-gray-300"
      />
    </form>
  </>
}