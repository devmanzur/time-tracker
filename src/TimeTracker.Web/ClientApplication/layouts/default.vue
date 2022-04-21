<template>
  <div class='flex'>
    <nav-bar style='position:fixed' />
    <div class='container p-5'>
      <div>
        <nuxt v-if='isLoggedIn' class='ml-64' />
        <div v-else class='ml-64 text-center'>
          <empty-state
            image-url='/img/unlock_account.svg'
            title='You are not signed in!'
            description='Please sign in to use the application'
          />
          <button
            class='
          bg-blue-500
          hover:bg-blue-700
          text-white
          font-bold
          py-2
          px-4
          rounded
          ml-4
          mt-4
        '
            @click='signIn'
          >
            Sign in
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import 'flowbite';

export default {
  name: 'DefaultLayout',
  computed: {
    isLoggedIn() {
      return this.$auth.loggedIn;
    }
  },
  methods: {
    signIn() {
      this.$router.push('/auth/login');
    }
  }
};
</script>

<style>

</style>
