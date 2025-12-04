<script setup lang="ts">
import { ref, nextTick } from 'vue'

interface Message {
  id: number
  text: string
  sender: 'user' | 'bot'
  timestamp: Date
}

const messages = ref<Message[]>([
  {
    id: 1,
    text: 'Hello! I\'m your AI assistant. How can I help you today?',
    sender: 'bot',
    timestamp: new Date()
  }
])

const inputMessage = ref('')
const isLoading = ref(false)
const chatContainer = ref<HTMLElement>()

let messageIdCounter = 2

const scrollToBottom = () => {
  nextTick(() => {
    if (chatContainer.value) {
      chatContainer.value.scrollTop = chatContainer.value.scrollHeight
    }
  })
}

const sendMessage = async () => {
  if (!inputMessage.value.trim() || isLoading.value) return

  const userMessage: Message = {
    id: messageIdCounter++,
    text: inputMessage.value,
    sender: 'user',
    timestamp: new Date()
  }

  messages.value.push(userMessage)
  const userInput = inputMessage.value
  inputMessage.value = ''
  scrollToBottom()

  isLoading.value = true

  try {
    const response = await fetch('http://localhost:5000/api/chat', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        conversationId: null, // Could track this for conversation history
        message: userInput
      })
    })

    if (!response.ok) {
      throw new Error('Failed to get response from server')
    }

    const data = await response.json()

    const botMessage: Message = {
      id: messageIdCounter++,
      text: data.response,
      sender: 'bot',
      timestamp: new Date(data.timestamp)
    }

    messages.value.push(botMessage)
    scrollToBottom()
  } catch (error) {
    const errorMessage: Message = {
      id: messageIdCounter++,
      text: 'Sorry, I encountered an error. Please try again.',
      sender: 'bot',
      timestamp: new Date()
    }
    messages.value.push(errorMessage)
  } finally {
    isLoading.value = false
  }
}

const handleKeyPress = (event: KeyboardEvent) => {
  if (event.key === 'Enter' && !event.shiftKey) {
    event.preventDefault()
    sendMessage()
  }
}

const formatTime = (date: Date) => {
  return date.toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit' })
}
</script>

<template>
  <div class="chatbot">
    <div class="chat-header">
      <h2>💬 AI Chatbot</h2>
      <p>Powered by vibecoding</p>
    </div>

    <div ref="chatContainer" class="chat-messages">
      <div
        v-for="message in messages"
        :key="message.id"
        :class="['message', message.sender]"
      >
        <div class="message-content">
          <p>{{ message.text }}</p>
          <span class="timestamp">{{ formatTime(message.timestamp) }}</span>
        </div>
      </div>

      <div v-if="isLoading" class="message bot typing">
        <div class="message-content">
          <div class="typing-indicator">
            <span></span>
            <span></span>
            <span></span>
          </div>
        </div>
      </div>
    </div>

    <div class="chat-input">
      <input
        v-model="inputMessage"
        type="text"
        placeholder="Type your message..."
        @keypress="handleKeyPress"
        :disabled="isLoading"
      />
      <button @click="sendMessage" :disabled="isLoading || !inputMessage.trim()">
        Send
      </button>
    </div>
  </div>
</template>

<style scoped>
.chatbot {
  max-width: 800px;
  margin: 2rem auto;
  background: white;
  border-radius: 12px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  height: 600px;
}

.chat-header {
  padding: 1.5rem;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border-radius: 12px 12px 0 0;
}

.chat-header h2 {
  margin: 0 0 0.5rem 0;
  font-size: 1.5rem;
}

.chat-header p {
  margin: 0;
  opacity: 0.9;
  font-size: 0.9rem;
}

.chat-messages {
  flex: 1;
  overflow-y: auto;
  padding: 1.5rem;
  display: flex;
  flex-direction: column;
  gap: 1rem;
  background: #f8f9fa;
}

.message {
  display: flex;
  animation: slideIn 0.3s ease-out;
}

@keyframes slideIn {
  from {
    opacity: 0;
    transform: translateY(10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.message.user {
  justify-content: flex-end;
}

.message.bot {
  justify-content: flex-start;
}

.message-content {
  max-width: 70%;
  padding: 0.75rem 1rem;
  border-radius: 12px;
  position: relative;
}

.message.user .message-content {
  background: #667eea;
  color: white;
}

.message.bot .message-content {
  background: white;
  color: #333;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.message-content p {
  margin: 0 0 0.5rem 0;
  word-wrap: break-word;
}

.timestamp {
  font-size: 0.75rem;
  opacity: 0.7;
}

.typing-indicator {
  display: flex;
  gap: 4px;
  padding: 8px 0;
}

.typing-indicator span {
  width: 8px;
  height: 8px;
  background: #999;
  border-radius: 50%;
  animation: bounce 1.4s infinite ease-in-out;
}

.typing-indicator span:nth-child(1) {
  animation-delay: -0.32s;
}

.typing-indicator span:nth-child(2) {
  animation-delay: -0.16s;
}

@keyframes bounce {
  0%, 80%, 100% {
    transform: translateY(0);
  }
  40% {
    transform: translateY(-10px);
  }
}

.chat-input {
  display: flex;
  padding: 1.5rem;
  gap: 1rem;
  border-top: 1px solid #e0e0e0;
  background: white;
  border-radius: 0 0 12px 12px;
}

.chat-input input {
  flex: 1;
  padding: 0.75rem 1rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  font-size: 1rem;
  outline: none;
  transition: border-color 0.3s;
}

.chat-input input:focus {
  border-color: #667eea;
}

.chat-input input:disabled {
  background: #f5f5f5;
  cursor: not-allowed;
}

.chat-input button {
  padding: 0.75rem 2rem;
  background: #667eea;
  color: white;
  border: none;
  border-radius: 8px;
  font-size: 1rem;
  cursor: pointer;
  transition: background 0.3s;
}

.chat-input button:hover:not(:disabled) {
  background: #5568d3;
}

.chat-input button:disabled {
  background: #ccc;
  cursor: not-allowed;
}
</style>
