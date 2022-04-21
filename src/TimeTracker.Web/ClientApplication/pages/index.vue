<template>
  <div>

    <div class='flex flex-col'>
      <div class='overflow-x-auto sm:-mx-6 lg:-mx-8'>
        <div class='inline-block py-2 min-w-full sm:px-6 lg:px-8'>
          <div class='overflow-hidden shadow-md sm:rounded-lg'>
            <table class='min-w-full'>
              <thead class='bg-gray-50 dark:bg-gray-700'>
              <tr>
                <th scope='col'
                    class='py-3 px-6 text-xs font-medium tracking-wider text-left text-gray-700 uppercase dark:text-gray-400'>
                  Name
                </th>
                <th scope='col'
                    class='py-3 px-6 text-xs font-medium tracking-wider text-left text-gray-700 uppercase dark:text-gray-400'>
                  Color
                </th>
                <th scope='col'
                    class='py-3 px-6 text-xs font-medium tracking-wider text-left text-gray-700 uppercase dark:text-gray-400'>
                  Category
                </th>
                <th scope='col'
                    class='py-3 px-6 text-xs font-medium tracking-wider text-left text-gray-700 uppercase dark:text-gray-400'>
                  Price
                </th>
                <th scope='col' class='relative py-3 px-6'>
                  <span class='sr-only'>Edit</span>
                </th>
              </tr>
              </thead>
              <tbody>

              <tr class='bg-white border-b dark:bg-gray-800 dark:border-gray-700'>
                <td class='py-4 px-6 text-sm font-medium text-gray-900 whitespace-nowrap dark:text-white'>
                  Apple MacBook Pro 17"
                </td>
                <td class='py-4 px-6 text-sm text-gray-500 whitespace-nowrap dark:text-gray-400'>
                  Sliver
                </td>
                <td class='py-4 px-6 text-sm text-gray-500 whitespace-nowrap dark:text-gray-400'>
                  Laptop
                </td>
                <td class='py-4 px-6 text-sm text-gray-500 whitespace-nowrap dark:text-gray-400'>
                  $2999
                </td>
                <td class='py-4 px-6 text-sm font-medium text-right whitespace-nowrap'>
                  <a href='#' class='text-blue-600 dark:text-blue-500 hover:underline'>Edit</a>
                </td>
              </tr>

              <tr class='bg-white border-b dark:bg-gray-800 dark:border-gray-700'>
                <td class='py-4 px-6 text-sm font-medium text-gray-900 whitespace-nowrap dark:text-white'>
                  Apple Imac 27"
                </td>
                <td class='py-4 px-6 text-sm text-gray-500 whitespace-nowrap dark:text-gray-400'>
                  White
                </td>
                <td class='py-4 px-6 text-sm text-gray-500 whitespace-nowrap dark:text-gray-400'>
                  Desktop Pc
                </td>
                <td class='py-4 px-6 text-sm text-gray-500 whitespace-nowrap dark:text-gray-400'>
                  $1999
                </td>
                <td class='py-4 px-6 text-sm font-medium text-right whitespace-nowrap'>
                  <a href='#' class='text-blue-600 dark:text-blue-500 hover:underline'>Edit</a>
                </td>
              </tr>

              <tr class='bg-white dark:bg-gray-800'>
                <td class='py-4 px-6 text-sm font-medium text-gray-900 whitespace-nowrap dark:text-white'>
                  Apple Magic Mouse 2
                </td>
                <td class='py-4 px-6 text-sm text-gray-500 whitespace-nowrap dark:text-gray-400'>
                  White
                </td>
                <td class='py-4 px-6 text-sm text-gray-500 whitespace-nowrap dark:text-gray-400'>
                  Accessories
                </td>
                <td class='py-4 px-6 text-sm text-gray-500 whitespace-nowrap dark:text-gray-400'>
                  $99
                </td>
                <td class='py-4 px-6 text-sm font-medium text-right whitespace-nowrap'>
                  <a href='#' class='text-blue-600 dark:text-blue-500 hover:underline'>Edit</a>
                </td>
              </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>

  </div>
</template>

<script>
export default {
  name: 'IndexPage'
};
</script>
