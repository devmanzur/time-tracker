<template>
  <div>
    <div class='min-h-screen bg-indigo-500 py-6 flex flex-col justify-center relative overflow-hidden sm:py-12'>
      <div
        class='relative px-6 pt-10 pb-8 bg-white shadow-xl ring-1 ring-gray-900/5 sm:max-w-lg sm:mx-auto sm:rounded-lg sm:px-10 w-screen'>
        <div>
          <form @submit.prevent='userLogin'>
            <div class='mb-6'>
              <label for='email' class='block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300'>Your
                email</label>
              <input
                id='email' v-model='login.username' type='email'
                class='bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'
                placeholder='name@flowbite.com' required=''>
            </div>
            <div class='mb-6'>
              <label for='password' class='block mb-2 text-sm font-medium text-gray-900 dark:text-gray-300'>Your
                password</label>
              <input
                id='password' v-model='login.password' type='password'
                class='bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'
                required=''>
            </div>
            <div class='flex items-start mb-6'>
              <div class='flex items-center h-5'>
                <input
                  id='remember' aria-describedby='remember' type='checkbox'
                  class='w-4 h-4 bg-gray-50 rounded border border-gray-300 focus:ring-3 focus:ring-blue-300 dark:bg-gray-700 dark:border-gray-600 dark:focus:ring-blue-600 dark:ring-offset-gray-800'
                  required=''>
              </div>
              <div class='ml-3 text-sm'>
                <label for='remember' class='font-medium text-gray-900 dark:text-gray-300'>Remember me</label>
              </div>
            </div>
            <button
              type='submit'
              class='text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800'>
              Submit
            </button>
          </form>

        </div>
      </div>
    </div>


  </div>
</template>

<script>

export default {
  name: 'LoginPage',
  layout: 'empty',
  data() {
    return {
      login: {
        username: '',
        password: ''
      }
    };
  },

  methods: {
    async userLogin() {
      try {
        const form = new URLSearchParams();
        form.append('username', this.login.username);
        form.append('password', this.login.password);
        form.append('grant_type', 'password');
        form.append('scope', 'offline_access');

        await this.$auth.loginWith('local', {
          data: form
        });
      } catch (err) {
        console.log(err);
      }
    }
  }
};
</script>

<style scoped>

</style>
