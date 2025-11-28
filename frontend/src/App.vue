<template>
  <div id="app">
    <div class="container">
      <h1>Chat Application</h1>

      <!-- Chat messages -->
      <div class="messages-container" ref="messagesContainer">
        <div v-for="msg in messages" :key="msg.id" :class="['message', msg.isBot ? 'bot' : 'user']">
          <div class="message-header">
            <strong>{{ msg.userName }}</strong>
            <span class="timestamp">{{ formatTime(msg.timestamp) }}</span>
          </div>
          <div class="message-content">{{ msg.content }}</div>
        </div>
      </div>

      <!-- Input area -->
      <div class="input-area">
        <input
          v-model="currentMessage"
          @keyup.enter="sendMessage"
          placeholder="Type a message..."
          class="message-input"
        />
        <button @click="sendMessage" class="send-btn">Send</button>
      </div>

      <!-- Status indicator -->
      <div v-if="loading" class="status">Sending...</div>
      <div v-if="error" class="error">{{ error }}</div>
    </div>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue';

export default {
  name: 'App',
  setup() {
    const messages = ref([]);
    const currentMessage = ref('');
    const loading = ref(false);
    const error = ref('');
    const messagesContainer = ref(null);

    // API base URL - TODO: move to config
    const API_URL = 'http://localhost:5000';

    // Load messages when component mounts
    onMounted(async () => {
      await loadMessages();
    });

    // Load message history from server
    const loadMessages = async () => {
      try {
        const response = await fetch(`${API_URL}/api/messages`);
        if (!response.ok) throw new Error('Failed to load messages');
        const data = await response.json();
        messages.value = data;

        // Scroll to bottom
        setTimeout(() => {
          if (messagesContainer.value) {
            messagesContainer.value.scrollTop = messagesContainer.value.scrollHeight;
          }
        }, 100);
      } catch (err) {
        error.value = 'Could not load messages';
        console.error(err);
      }
    };

    // Send message to server
    const sendMessage = async () => {
      if (!currentMessage.value.trim()) return;

      loading.value = true;
      error.value = '';

      try {
        const response = await fetch(`${API_URL}/api/chat`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            message: currentMessage.value,
            userName: 'User' // FIXME: get from user profile?
          })
        });

        if (!response.ok) throw new Error('Failed to send message');

        const data = await response.json();

        // Clear input
        currentMessage.value = '';

        // Reload messages to get both user and bot messages
        await loadMessages();

      } catch (err) {
        error.value = 'Failed to send message. Try again.';
        console.error(err);
      } finally {
        loading.value = false;
      }
    };

    // Format timestamp - copied from old project
    const formatTime = (timestamp) => {
      const date = new Date(timestamp);
      const hours = date.getHours().toString().padStart(2, '0');
      const minutes = date.getMinutes().toString().padStart(2, '0');
      return `${hours}:${minutes}`;
    };

    return {
      messages,
      currentMessage,
      loading,
      error,
      messagesContainer,
      sendMessage,
      formatTime
    };
  }
};
</script>

<style scoped>
#app {
  font-family: Arial, sans-serif;
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
}

.container {
  background: #f5f5f5;
  border-radius: 8px;
  padding: 20px;
}

h1 {
  color: #333;
  margin-bottom: 20px;
}

.messages-container {
  height: 400px;
  overflow-y: auto;
  background: white;
  border: 1px solid #ddd;
  border-radius: 4px;
  padding: 15px;
  margin-bottom: 15px;
}

.message {
  margin-bottom: 15px;
  padding: 10px;
  border-radius: 4px;
}

.message.user {
  background: #e3f2fd;
  margin-left: 20%;
}

.message.bot {
  background: #f5f5f5;
  margin-right: 20%;
}

.message-header {
  display: flex;
  justify-content: space-between;
  margin-bottom: 5px;
  font-size: 12px;
  color: #666;
}

.message-content {
  color: #333;
}

.input-area {
  display: flex;
  gap: 10px;
}

.message-input {
  flex: 1;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
}

.send-btn {
  padding: 10px 20px;
  background: #1976d2;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.send-btn:hover {
  background: #1565c0;
}

.status {
  margin-top: 10px;
  color: #666;
  font-size: 14px;
}

.error {
  margin-top: 10px;
  color: #d32f2f;
  font-size: 14px;
}

/* Old styles - keeping for backwards compatibility */
/* .old-message-style { ... } */
</style>
